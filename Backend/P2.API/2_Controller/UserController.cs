namespace P2.API.Controller;
using Microsoft.AspNetCore.Mvc;
using P2.API.Service;
using P2.API.Model;
using Microsoft.AspNetCore.JsonPatch;

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
    public IActionResult AddNewUser([FromBody] User user)
    {
        try{
            _userService.NewUser(user);
            return Ok(user);
        }
        catch(Exception){
            return BadRequest("Could not add user");
        }
    }

    [HttpDelete]
    public IActionResult RemoveUser(int id)
    {
        try{
            User? user = _userService.GetUserById(id);
            if (user != null)
            {
                //return not found
                _userService.DeleteUser(user);
                return Ok();
            }
            return NotFound();
        }
        catch(Exception){
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
            if(editUser == null)
            {
                return NotFound();
            }

            userPatch.ApplyTo(editUser, ModelState);
            _userService.EditUser(editUser);
            if(!ModelState.IsValid)
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
}