namespace P2.API.Repository;
using P2.API.Model;
using P2.API.Data;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly BacklogContext _userContext;

    public UserRepository(BacklogContext userContext) => _userContext = userContext;
    //should be able to get all users
    public IEnumerable<User> GetAllUsers()
    {
        return _userContext.Users.ToList();
    }
    //should be able to get users by id
    public User? GetUserById(int id)
    {
        //should null logic be here?
        return _userContext.Users.Find(id);
        //make it nullable?
    }
    //could get users by username?
    public User? GetUserByUsername(string username)
    {
        return _userContext.Users.Where(u => u.UserName.Equals(username)).FirstOrDefault();

    }
    //should be able to create a new user
    public User NewUser(User user)
    {
        _userContext.Users.Add(user);
        _userContext.SaveChanges();
        return user;
    }

    //should be able to edit user (not whole thing though, so ideally patch > put)
    //TODO: 
    public void EditUser(User user)
    {
        _userContext.Entry(user).State = EntityState.Modified;
        _userContext.SaveChanges();
    }

    //should be able to delete a user
    public void DeleteUser(User deleteUser)
    {
        _userContext.Users.Remove(deleteUser); //should I include logic here for not found? Or should I call find on the controller and then call this if a user was found?
        _userContext.SaveChanges();
    }
}