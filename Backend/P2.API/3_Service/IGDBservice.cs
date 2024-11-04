
using P2.API.Model;
using P2.API.Repository;
using System.Text.Json;
using System.Net.Http;
using System.Text;
namespace P2.API.Service;

public class IGDBService {
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private string token;
    private int expiration;
    private string clientId;
    private readonly IConfiguration _configuration;

    public IGDBService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public void GetIgdbAccessToken()
    {
        if(string.IsNullOrEmpty(token) || DateTime.Compare(new DateTime(expiration), DateTime.Now) <= 0)
        {
            var requestUrl = "https://id.twitch.tv/oauth2/token";
            clientId = _configuration["IGDB:id"];
            var clientSecret = _configuration["IGDB:secret"];

            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PostAsync(requestUrl, requestContent).Result;
                response.EnsureSuccessStatusCode();
                var jsonString = response.Content.ReadAsStringAsync().Result;
                using (var jsonDoc = JsonDocument.Parse(jsonString))
                {
                    token = jsonDoc.RootElement.GetProperty("access_token").GetString();
                    expiration = jsonDoc.RootElement.GetProperty("expires_in").GetInt32();
                }
            }
        } 
    }

    /**
    * Should find 10 games that match the string (number is low for testing purposes for now, once we're sure it works properly we can increase it)
    */
    public List<Game> GetGamesByName(string name)
    {
        GetIgdbAccessToken();
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Client-ID", clientId);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "P2GameBoardTracker/1.0");
        Console.WriteLine("ARE THESE FINE????");
        Console.WriteLine($"Client ID: {clientId}");
        Console.WriteLine($"Authorization: Bearer {token}");
        //IGDB API gets info... by using post?
        string rawString = $"fields id, name, summary, total_rating; limit 10; search \"{name}\";";
        var response =  _httpClient.PostAsync("games", new StringContent(rawString, Encoding.UTF8, "text/plain")).Result;
        Console.WriteLine(response.ToString());
        response.EnsureSuccessStatusCode();
        var jsonString = response.Content.ReadAsStringAsync().Result;
        var games = JsonSerializer.Deserialize<List<Game>>(jsonString);
        // var games = new List<Game>();
        //     using (JsonDocument doc = JsonDocument.Parse(jsonString))
        //     {
        //         if (doc.RootElement.TryGetProperty("body", out JsonElement results))
        //         {
        //             foreach (var game in results.EnumerateArray())
        //             {
        //                 int id = game.GetProperty("id").GetInt32();
        //                 string gameName = game.GetProperty("name").GetString();
        //                 games.Add(new Game {
        //                     GameId = id, 
        //                     Name = gameName
        //                 });
        //             }
        //         }             
        // }
        return games;
    }
}