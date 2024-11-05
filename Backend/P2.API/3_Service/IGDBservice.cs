
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
    * Should find 10 games that match the string + any genres or platforms they want to filter by
    * (number is low for testing purposes for now, once we're sure it works properly we can increase it)
    */
    public List<Game> GetGamesFiltered(string name, List<int>? genres = null, List<int>? platforms = null, int limit = 1)
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
        //ADD WAYS TO FILTER BY GENRE AND PLATFORM IF PROVIDED

        string rawString = $"fields id, name, summary, total_rating; limit {limit}; search \"{name}\"";
        bool genresFiltered = false;

        
        //append to raw string if the list of genreids is not empty or null
        //have boolean to show this has been applied
        if(genres != null && genres.Count > 0)
        {
            string separatedGenres = string.Join(", ", genres);
            rawString += $"; where genres = ({separatedGenres})";
            genresFiltered = true;
        }
        //append to raw string if the list of platforms is not empty or null
        //also decide between where or & depending on if the previews filters were applied or no
        if(platforms != null && platforms.Count > 0)
        {
            string separatedPlatforms = string.Join(", ", platforms);
            if(genresFiltered)
            {
                
                rawString += $"& platforms = ({separatedPlatforms});";
            }
            else
            {
                rawString += $"; where platforms = ({separatedPlatforms});";
            }
        }
        rawString += ";";
        var response =  _httpClient.PostAsync("games", new StringContent(rawString, Encoding.UTF8, "text/plain")).Result;
        Console.WriteLine(response.ToString());
        response.EnsureSuccessStatusCode();
        var jsonString = response.Content.ReadAsStringAsync().Result;
        var games = JsonSerializer.Deserialize<List<Game>>(jsonString);
        //patch every single game to set image url?
        foreach(Game game in games)
        {
            game.ImageURL = getImageUrl(game.GameId);
        }
        return games;
    }

    /**
    * Creates the url based on the game's id
    */
    private string getImageUrl(int gameId)
    {
        //just get imageid based on game id?
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Client-ID", clientId);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "P2GameBoardTracker/1.0");
        string rawString = $"fields image_id; limit 1; where game = {gameId};";
        var response =  _httpClient.PostAsync("covers", new StringContent(rawString, Encoding.UTF8, "text/plain")).Result;
        response.EnsureSuccessStatusCode();
        var jsonString = response.Content.ReadAsStringAsync().Result;
        string imageId = "";
        using (JsonDocument document = JsonDocument.Parse(jsonString))
        {
            var root = document.RootElement;
            imageId = root[0].GetProperty("image_id").GetString();
        }
        //retrieve cover by imageid and create url
        return $"https://images.igdb.com/igdb/image/upload/t_cover_big/{imageId}.jpg";
    }

    //Method to retrieve all genres/platforms and their ids and return a list?


    // This seems to get pretty popular games, should be useful for initial data fecth on model creation
    // fields <whatever goes in here>; sort total_rating_count desc;
    //search name already sorts by relevancy so initial fetch on model creating needs different method
}