using P2.API.Service;
using P2.API.Data;
using P2.API.Model;
using P2.API.Repository;
using Moq;
using Microsoft.IdentityModel.Tokens;
namespace P2.TEST;

public class GameServiceTest
{
    private Mock<IGameRepository> mockRepo;
    private GameService  gameService;
    public GameServiceTest()
    {
        mockRepo = new();
        gameService = new(mockRepo.Object);
    }

    [Fact]
    public void GetAllGamesOnEmpty()
    {
       
        //Arrange

        List<Game> gameList = [];

        mockRepo.Setup(repo => repo.GetAllGames())
            .Returns(gameList);

        //Act
        var result = gameService.GetAllGames();
        
        //Assert
        Assert.True(result.IsNullOrEmpty());
    }

    [Fact]
    public void GetAllGames()
    {

        List<Game> gameList = [new Game {Name = "test1"},
        new Game {Name = "test2"}];

        mockRepo.Setup(repo => repo.GetAllGames())
            .Returns(gameList);

        //Act
        var result = gameService.GetAllGames();
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, u => u.Name.Equals("test1"));
        Assert.Contains(result, u => u.Name.Equals("test2"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void GetGameByIdFound(int id)
    {

        List<Game> gameList = [new Game {GameId = 0, Name = "test1"},
        new Game {GameId = 1, Name = "test2"}];

        mockRepo.Setup(repo => repo.GetGameById(It.IsAny<int>()))
            .Returns(gameList.FirstOrDefault(game => game.GameId == id));

        //Act
        var result = gameService.GetGameById(id);
        Assert.Equal(result, gameList[id]);
    }
    [Theory]
    [InlineData(3)]
    [InlineData(50)]
    [InlineData(10)]
    public void GetGameByIdNotFound(int id)
    {

        List<Game> gameList = [new Game {Name = "test1"},
        new Game {Name = "test2"}];

        mockRepo.Setup(repo => repo.GetGameById(It.IsAny<int>()))
            .Returns(gameList.FirstOrDefault(game => game.GameId == id));

        //Act
        //Assert
        Assert.Null(gameService.GetGameById(id));
    }
    [Fact]
    public void NewGameSuccessful()
    {
        List<Game> gameList = [];
        Game newgame = new Game{Name = "added"};
        mockRepo.Setup(repo => repo.NewGame(It.IsAny<Game>()))
            .Callback(() => gameList.Add(newgame))
            .Returns(newgame);
        var result = gameService.NewGame(newgame);

        Assert.False(gameList.IsNullOrEmpty());
        Assert.Contains(result.Name, "added");
    }

    [Fact]
    public void NewGameFailed()
    {
        List<Game> gameList = [];
        //purposefully made null to check for exception!! Will give out warnings
        //Can they be suppressed like Java?
        Game newgame = new Game{Name = null};
        mockRepo.Setup(repo => repo.NewGame(It.IsAny<Game>()))
            .Callback(() => gameList.Add(newgame))
            .Returns(newgame);
        Assert.Throws<Exception>(()=>gameService.NewGame(newgame));
    }

    //game service does not check for invalid deletes, controller handles that logic
    [Fact]
    public void DeleteGame()
    {
        List<Game> gameList = [new Game {GameId = 0, Name = "test1"},
        new Game {GameId = 1, Name = "deleteThis"}];
        mockRepo.Setup(repo => repo.DeleteGame(It.IsAny<Game>()));
        gameService.DeleteGame(gameService.GetGameById(1));
        // Assert.DoesNotContain(gameList, u => u.gameName.Equals("deleteThis"));
    }

    [Fact]
    public void GetGamesBygamenameSuccess()
    {
        //THIS ONE IS VERY DIFFERENT: APPROXIMATION INSTEAD OF EXACT SEARCH
        // List<Game> gameList = [new Game {GameId = 0, Name = "test1"},
        // new Game {GameId = 1, Name = "test2"}];

        // mockRepo.Setup(repo => repo.GetGameSByName(It.IsAny<string>()))
        //     .Returns(gameList.FirstOrDefault(game => game.gameName.Equals("test1")));

        // //Act
        // var result = gameService.GetgameBygamename("test1");
        // //Assert
        // Assert.Equal(result, gameList[0]);
    }
    [Fact]
    public void GetGameSByGameNameFailed()
    {
        // List<game> gameList = [new game {gameName = "test1", Password = "password1"},
        // new game {gameName = "test2", Password = "password2"}];

        // mockRepo.Setup(repo => repo.GetgameBygamename(It.Is<string>(u => u == "not on list")))
        //     .Returns((game?) null);

        // //Act
        // var result = gameService.GetgameBygamename("not in list");
        // //Assert
        // Assert.Null(result);
    }
}
