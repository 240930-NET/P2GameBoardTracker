using System.Text.Json.Serialization;
namespace P2.API.Model;

public class Game {
    [JsonPropertyName("id")]
    public int GameId{get; set;}

    [JsonPropertyName("name")]
    public required string Name{get;set;}

    [JsonPropertyName("summary")]
    public string Description{get;set;} = "";
    [JsonPropertyName("total_rating")]
    public double Rating{get;set;} = 0.0;
    
    public string ImageURL{get;set;} ="";


}