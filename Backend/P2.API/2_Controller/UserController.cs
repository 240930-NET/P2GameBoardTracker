namespace P2.API.Controller;
using Microsoft.AspNetCore.Mvc;
using P2.API.Service;
using P2.API.Model;
using P2.API.Model.DTO;
using Microsoft.AspNetCore.JsonPatch;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

[Route("api/User")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;
	public UserController(IUserService userService) => _userService = userService;

	[HttpGet]
	public IActionResult GetAllUsers()
	{
		var users = _userService.GetAllUsers();
		return Ok(users);
	}

	[HttpGet("{id}")]
	public IActionResult GetUserById(int id)
	{
		var user = _userService.GetUserById(id);
		if (user == null)
		{
			//return not found
			return NotFound();
		}
		return Ok(user);
	}

	[HttpGet("/GetUserByUsername/{username}")]
	public IActionResult GetUserByUsername(string username)
	{
		var user = _userService.GetUserByUsername(username);
		if (user == null)
		{
			//return not found
			return NotFound();
		}
		return Ok(user);
	}

	[HttpPost]
	// changed type to userDTO because id autoimplements anyway 
	public IActionResult AddNewUser([FromBody] UserDto userDto)
	{
		try
		{
			var newUser = _userService.NewUser(userDto);
			return Ok(newUser);
		}
		catch (Exception)
		{
			return BadRequest("Could not add user");
		}
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] UserDto registrationDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		try
		{
			var existingUser = _userService.GetUserByUsername(registrationDto.UserName);
			if (existingUser != null)
			{
				return Conflict("Username already exists");
			}

			var newUser = _userService.NewUser(registrationDto);
			await UserSessionAndAuth(newUser);

			return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, CreateUserDataToReturn(newUser));
		}
		catch (Exception ex)
		{
			// Log the exception
			return StatusCode(500, "An error occurred while registering the user");
		}
	}

	[HttpDelete]
	public IActionResult RemoveUser(int id)
	{
		try
		{
			User? user = _userService.GetUserById(id);
			if (user != null)
			{
				//return not found
				_userService.DeleteUser(user);
				return Ok();
			}
			return NotFound();
		}
		catch (Exception)
		{
			return BadRequest("Could not delete user");
		}
	}

	//Body in swagger should be something like:
	//     [
	//   {
	//     "op": "replace",
	//     "path": "/username",
	//     "value": "patchedValue"
	//   }]
	[HttpPatch("{id}")]
	public IActionResult EditUser(int id, [FromBody] JsonPatchDocument<User> userPatch)
	{
		if (userPatch != null)
		{
			User? editUser = _userService.GetUserById(id);
			if (editUser == null)
			{
				return NotFound();
			}

			userPatch.ApplyTo(editUser, ModelState);
			_userService.EditUser(editUser);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(editUser);
		}
		else
		{
			return BadRequest(ModelState);
		}
	}

	// Authenticates the user and creates a session 
	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] UserDto loginDto)
	{
		var user = _userService.AuthenticateUser(loginDto.UserName, loginDto.Password);
		if (user == null)
		{
			// user is unauthorized to enter 401 authentication fails 
			return Unauthorized("Invalid Username or Password");
		}

		// update the last login date 
		user.LastLoginDate = DateTime.UtcNow;
		_userService.EditUser(user);

		// set up the session and auth 
		await UserSessionAndAuth(user);

		return Ok(new { message = "Login Successful", userName = CreateUserDataToReturn(user) });
	}

	[HttpPost("Logout")]
	// Logs out the current user and clears the session 
	public async Task<IActionResult> Logout()
	{
		// clear the session data 
		HttpContext.Session.Clear();
		// log the user out 

		await HttpContext.SignOutAsync();
		// Delete the session cookie 
		Response.Cookies.Delete("SessionId");
		return Ok(new { message = "Logged out successfully" });
	}

	[HttpGet("current")]
	[Authorize]
	public IActionResult GetCurrentUser()
	{
		var userId = HttpContext.Session.GetInt32("UserId");
		if (!userId.HasValue)
		{
			return Unauthorized("No user is currently logged in");
		}

		var user = _userService.GetUserById(userId.Value);
		if (user == null)
		{
			return NotFound("User not found");
		}

		return Ok(CreateUserDataToReturn(user));
	}

	private async Task UserSessionAndAuth(User user)
	{
		HttpContext.Session.SetInt32("UserId", user.UserId);
		HttpContext.Session.SetString("UserName", user.UserName);

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, user.UserName),
			new(ClaimTypes.NameIdentifier, user.UserId.ToString())
		};

		var identity = new ClaimsIdentity(claims, "login");
		var principal = new ClaimsPrincipal(identity);

		await HttpContext.SignInAsync(principal);
	}

	private object CreateUserDataToReturn(User user)
	{
		return new
		{
			user.UserId,
			user.UserName,
			user.LastLoginDate
		};
	}
}