using P2.API.Model;
using P2.API.Repository;

namespace P2.API.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) => _userRepository = userRepository;

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public User? GetUserById(int id)
    {
        return _userRepository.GetUserById(id);
    }

    public User NewUser(User user){
        if(user.UserName != null && user.Password != null)
        {
            return _userRepository.NewUser(user);
        }
        else{
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
}