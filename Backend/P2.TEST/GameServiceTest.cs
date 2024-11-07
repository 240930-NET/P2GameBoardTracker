using P2.API.Service;
using P2.API.Data;
using P2.API.Model;
using P2.API.Model.DTO;
using P2.API.Repository;
using Moq;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Microsoft.Extensions.Configuration;
namespace P2.TEST;

public class GameServiceTest
{
    private Mock<IGameRepository> mockRepo;
    private GameService  gameService;
    private readonly Mock<IMapper> _mockedMapper;
    private readonly  Mock<IGDBService> mockIGDBService;
    public GameServiceTest()
    {
        //mock igdbservice for this and user test and then have an actual unit test class for the proper external api calls
        mockRepo = new();
        _mockedMapper = new Mock<IMapper>();
        var secrets = new Dictionary<string, string>
        {
            { "IGDB:id", "mockid" },
            { "IGDB:secret", "mocksecret" }
        };
        IConfiguration mockConfiguration = new ConfigurationBuilder()
            .AddInMemoryCollection(secrets)
            .Build();

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        var mockHttpClient = new HttpClient(mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("https://api.igdb.com")
        };
        mockIGDBService = new Mock<IGDBService>(mockHttpClient, mockConfiguration);
        gameService = new(mockRepo.Object, _mockedMapper.Object, mockIGDBService.Object);
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
        GameDto newgameDto = new GameDto{Name = "added"};
        Game newgame = new Game{Name = "added"};
        mockRepo.Setup(repo => repo.NewGame(It.IsAny<Game>()))
            .Callback(() => gameList.Add(newgame))
            .Returns(newgame);
        var result = gameService.NewGame(newgameDto);

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
        GameDto newgameDto = new GameDto{Name = null};
        mockRepo.Setup(repo => repo.NewGame(It.IsAny<Game>()))
            .Callback(() => gameList.Add(newgame));
        Assert.Throws<Exception>(()=>gameService.NewGame(newgameDto));
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
    public void GetGamesByNameSuccess()
    {
        //THIS ONE IS VERY DIFFERENT: APPROXIMATION INSTEAD OF EXACT SEARCH
        List<Game> gameList = [new Game {GameId = 0, Name = "test1"},
            new Game {GameId = 1, Name = "test2"}, new Game {GameId = 3, Name = "not"}];
        List<Game> reducedList = [new Game {GameId = 0, Name = "test1"},
            new Game {GameId = 1, Name = "test2"}];
        mockRepo.Setup(repo => repo.GetGamesByName(It.IsAny<string>()))
            .Returns(reducedList);

        //Act
        var result = gameService.GetGamesByName("tes");
        //Assert
        Assert.Equal(result, reducedList);
    }
    // [Fact]
    // public void GetGamesByNameNotInList()
    // {
        
    // List<Game> emptyList = [];
    // mockRepo.Setup(repo => repo.GetGamesByName(It.IsAny<string>()))
    //     .Returns(emptyList);
    // Game newGame = new Game { GameId = 1, Name = "not in list" };
    // List<Game> gameList = new List<Game> 
    // { 
    //     newGame
    // };
    // mockIGDBService.Protected().Setup(service => service.GetGamesFiltered(It.IsAny<string>(), It.IsAny<List<int>?>(), It.IsAny<List<int>?>(), It.IsAny<int>()))
    //     .Returns(gameList);
    // var result = gameService.GetGamesByName("not in list");
    // Assert.NotNull(result);
    // Assert.NotEmpty(result); 
    // Assert.Equal(gameList.Count, result.Count());
    // Assert.Contains(result, g => g.Name == "not in list");
    // }
}
