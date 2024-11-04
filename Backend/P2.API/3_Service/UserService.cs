using P2.API.Model;
using P2.API.Repository;
using P2.API.Model.DTO;
using AutoMapper;

namespace P2.API.Service;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public UserService(IUserRepository userRepository, IMapper
	mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;

	}

	public IEnumerable<User> GetAllUsers()
	{
		return _userRepository.GetAllUsers();
	}

	public User? GetUserById(int id)
	{
		return _userRepository.GetUserById(id);
	}

	public User NewUser(UserDto userDto)
	{
		if (userDto.UserName != null && userDto.Password != null)
		{
			var user = _mapper.Map<User>(userDto);
			user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
			
			return _userRepository.NewUser(user);
		}
		else
		{
			throw new Exception("Invalid user! Please input a username and password");
		}
	}

	public User? GetUserByUsername(string username)
	{
		return _userRepository.GetUserByUsername(username);
	}

	public void DeleteUser(User deleteUser)
	{
		_userRepository.DeleteUser(deleteUser);
	}

	public void EditUser(User user)
	{
		_userRepository.EditUser(user);
	}

	public User? AuthenticateUser(string username, string password)
	{
		var user =
		_userRepository.GetUserByUsername(username);
		if (user != null && VerifyPassword(password, user.Password))
		{
			return user;

		}
		return null;
	}

	public bool VerifyPassword(string password, string passwordHash
	)
	{
		return BCrypt.Net.BCrypt.Verify(password,passwordHash);
	}
	
	public void UpdatePassword(User user, string newPassword) { 
		user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword); 
		_userRepository.EditUser(user); 
	}
}