namespace P2.API.Repository;
using P2.API.Model;
using P2.API.Data;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly BacklogContext _userContext;

    public UserRepository(BacklogContext userContext) => _userContext = userContext;
    /**
    * Queries the User table for all entries
    */
    public IEnumerable<User> GetAllUsers()
    {
        return _userContext.Users.ToList();
    }
    /**
    * Queries the user table for a specific entry given an id
    */
    public User? GetUserById(int id)
    {
        return _userContext.Users.Find(id);
    }
    /**
    * Queries the user table given a username
    */
    public User? GetUserByUsername(string username)
    {
        return _userContext.Users.Where(u => u.UserName.Equals(username)).FirstOrDefault();

    }
    /**
    * Creates a new user entry given a user object is provided
    */
    public User NewUser(User user)
    {
        _userContext.Users.Add(user);
        _userContext.SaveChanges();
        return user;
    }

    /**
    * Edits a user entry given a user object is provided
    */
    public void EditUser(User user)
    {
        _userContext.Entry(user).State = EntityState.Modified;
        _userContext.SaveChanges();
    }

    /**
    * Removes a user from the table given a user object is provided
    */
    public void DeleteUser(User deleteUser)
    {
        _userContext.Users.Remove(deleteUser); 
        _userContext.SaveChanges();
    }
}