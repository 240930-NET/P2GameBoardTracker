namespace P2.API.Model;

public class Game {
    public int GameId{get; set;}

    //add steamid somewhere in here as well once interaction with the external API starts + playtime and img from steam api
    public required string Name{get;set;}
    public string Description{get;set;} = "";


}