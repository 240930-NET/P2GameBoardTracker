using Microsoft.EntityFrameworkCore;

namespace P2.API.Model;
[PrimaryKey(nameof(UserId), nameof(GameId))]
public class Backlog()
{
    //Backlog will be more of a junction table between user and its games. Will have extra data that shouldn't be stored
    //in the games object since it is specific to the user
    //these two are a joint PK
    public int UserId { get; set; }  // Foreign key for User

    public int GameId { get; set; }  // Foreign key for Game
    // public Game? Game { get; set; }
    // Additional properties for the game in the user's backlog
    public required bool Completed { get; set; }  

    public DateTime? CompletionDate { get; set; }  // When the game was completed
}