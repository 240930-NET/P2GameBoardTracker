using P1.API.Service;
using P1.API.Data;
using P1.API.Model;
using P1.API.Repository;
using Moq;
using Microsoft.IdentityModel.Tokens;
namespace P1.TEST;

public class UserServiceTest
{
    private Mock<IUserRepository> mockRepo;
    private UserService  userService;
    public UserServiceTest()
    {
        mockRepo = new();
        userService = new(mockRepo.Object);
    }

    [Fact]
    public void GetAllUsersOnEmpty()
    {
       
        //Arrange

        List<User> userList = [];

        mockRepo.Setup(repo => repo.GetAllUsers())
            .Returns(userList);

        //Act
        var result = userService.GetAllUsers();
        
        //Assert
        Assert.True(result.IsNullOrEmpty());
    }

    [Fact]
    public void GetAllUsers()
    {

        List<User> userList = [new User {UserName = "test1", Password = "password1"},
        new User {UserName = "test2", Password = "password2"}];

        mockRepo.Setup(repo => repo.GetAllUsers())
            .Returns(userList);

        //Act
        var result = userService.GetAllUsers();
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, u => u.UserName.Equals("test1"));
        Assert.Contains(result, u => u.Password.Equals("password1"));
        Assert.Contains(result, u => u.UserName.Equals("test2"));
        Assert.Contains(result, u => u.Password.Equals("password2"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GetUserByIdFound(int id)
    {

        List<User> userList = [new User {UserId = 0, UserName = "test1", Password = "password1"},
        new User {UserId = 1, UserName = "test2", Password = "password2"}];

        mockRepo.Setup(repo => repo.GetUserById(It.IsAny<int>()))
            .Returns(userList.FirstOrDefault(user => user.UserId == id));

        //Act
        var result = userService.GetUserById(id);
        Assert.Equal(result, userList[id]);
    }
    [Theory]
    [InlineData(3)]
    [InlineData(50)]
    [InlineData(10)]
    public void GetUserByIdNotFound(int id)
    {

        List<User> userList = [new User {UserName = "test1", Password = "password1"},
        new User {UserName = "test2", Password = "password2"}];

        mockRepo.Setup(repo => repo.GetUserById(It.IsAny<int>()))
            .Returns(userList.FirstOrDefault(user => user.UserId == id));

        //Act
        //Assert
        Assert.Null(userService.GetUserById(id));
    }
    [Fact]
    public void NewUserSuccessful()
    {
        List<User> userList = [];
        User newUser = new User{UserName = "added", Password = "add"};
        mockRepo.Setup(repo => repo.NewUser(It.IsAny<User>()))
            .Callback(() => userList.Add(newUser))
            .Returns(newUser);
        var result = userService.NewUser(newUser);

        Assert.False(userList.IsNullOrEmpty());
        Assert.Contains(result.UserName, "added");
        Assert.Contains(result.Password, "add");
    }

    [Fact]
    public void NewUserFailed()
    {
        List<User> userList = [];
        //purposefully made null to check for exception!! Will give out warnings
        //Can they be suppressed like Java?
        User newUser = new User{UserName = "user", Password = null};
        User newUser2 = new User{UserName = null, Password = "pass"};
        mockRepo.Setup(repo => repo.NewUser(It.IsAny<User>()))
            .Callback(() => userList.Add(newUser))
            .Returns(newUser);
        Assert.Throws<Exception>(()=>userService.NewUser(newUser));
        Assert.Throws<Exception>(()=>userService.NewUser(newUser2));
    }

    //User service does not check for invalid deletes, controller handles that logic
    [Fact]
    public void DeleteUser()
    {
        List<User> userList = [new User {UserId = 0, UserName = "test1", Password = "password1"},
        new User {UserId = 1, UserName = "deleteThis", Password = "password"}];
        mockRepo.Setup(repo => repo.DeleteUser(It.IsAny<User>()));
        userService.DeleteUser(userService.GetUserById(1));
        // Assert.DoesNotContain(userList, u => u.UserName.Equals("deleteThis"));
    }

    [Fact]
    public void GetUserByUsernameSuccess()
    {
        List<User> userList = [new User {UserId = 0, UserName = "test1", Password = "password1"},
        new User {UserId = 1, UserName = "test2", Password = "password2"}];

        mockRepo.Setup(repo => repo.GetUserByUsername(It.IsAny<string>()))
            .Returns(userList.FirstOrDefault(user => user.UserName.Equals("test1")));

        //Act
        var result = userService.GetUserByUsername("test1");
        //Assert
        Assert.Equal(result, userList[0]);
    }
    [Fact]
    public void GetUserByUsernameFailed()
    {
        List<User> userList = [new User {UserName = "test1", Password = "password1"},
        new User {UserName = "test2", Password = "password2"}];

        mockRepo.Setup(repo => repo.GetUserByUsername(It.Is<string>(u => u == "not on list")))
            .Returns((User?) null);

        //Act
        var result = userService.GetUserByUsername("not in list");
        //Assert
        Assert.Null(result);
    }

    //LOGIC FOR CHECKING THIS ONE IS ON CONTROLLER AS WELL
    //Editing itself is also in controller so this is hard to test...
    // [Fact]
    // public void EditUser()
    // {
    //     List<User> userList = [new User {UserName = "test1", Password = "password1"},
    //     new User {UserName = "test2", Password = "password2"}];
    //     User patchForUser = new User{Username =};
    //     mockRepo.Setup(repo => repo.EditUser(It.IsAny<User>()))
    //         .Returns(patchForUser);

    //     //Act
    //     var result = userService.GetUserByUsername("not in list");
    //     //Assert
    //     Assert.Null(result);
    // }
}