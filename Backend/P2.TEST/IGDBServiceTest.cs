using P2.API.Service;
using P2.API.Data;
using P2.API.Model;
using P2.API.Model.DTO;
using P2.API.Repository;
using Moq;
using Moq.Protected;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text.Json;
namespace P2.TEST;
public class IGDBServiceTest {

    private Mock<HttpMessageHandler> mockHttpMessageHandler;
    private HttpClient mockHttpClient;
    private IGDBService _igdbService;

    public IGDBServiceTest()
    {
        var secrets = new Dictionary<string, string>
        {
            { "IGDB:secret" , "4qyfmaabfjm4w9jvi7fme2wvy9t1b0" },
            { "IGDB:id" , "n8s9j1x0w281ebn3957urpclwdnagf" }
        };
        var mockConfiguration = new ConfigurationBuilder()
            .AddInMemoryCollection(secrets)
            .Build();

        mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpClient = new HttpClient(mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("https://api.igdb.com/v4/")
        };
        _igdbService = new IGDBService(mockHttpClient, mockConfiguration);
    }

    /**
    * Tests for get games filtered
    */ 
    [Fact]
    public void TestGetGamesFiltered()
    {
        var gameData = new[]
        {
            new 
            {
                id = 7325,
                name = "Lara Croft and the Temple of Osiris",
                summary = "Lara Croft and the Temple of Osiris is the sequel to Lara Croft and the Guardian of Light, and the second game in a spin-off from the main Tomb Raider series. The two-player cooperative campaign from the first game is expanded to support up to four players. They need to work together in a platform environment to explore the temple, defeat hordes of enemies from the Egyptian underworld, solve puzzles, and avoid traps, using a high-angle perspective like the previous game.",
                total_rating = 72.50013490215338
            }
        };
        var coverData = new[]
        {
            new 
            {
                image_id = "co1voe"
            }
        };
        string jsonPayload = JsonSerializer.Serialize(gameData);
        var mockGameResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonPayload)
        };
        jsonPayload = JsonSerializer.Serialize(coverData);
        var mockCoverResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonPayload)
        };
        mockHttpMessageHandler
            .Protected()
            .SetupSequence<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockGameResponse)
            .ReturnsAsync(mockCoverResponse);
        List<Game> games = _igdbService.GetGamesFiltered("lara");
        Assert.NotEmpty(games);
        Assert.Equal("Lara Croft and the Temple of Osiris", games[0].Name);
    }
}