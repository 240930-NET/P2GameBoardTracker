namespace P2.API.Repository;
using P2.API.Model;
using P2.API.Data;

public class GameRepository : IGameRepository
{
    private readonly BacklogContext _gameContext;

    public GameRepository(BacklogContext gameContext) => _gameContext = gameContext;
    /**
    * Queries all games from the table
    */
    public IEnumerable<Game> GetAllGames()
    {
        return _gameContext.Games.ToList();
    }

    /**
    * Finds a specific game entry given an ID
    */
    public Game? GetGameById(int id)
    {
        return _gameContext.Games.Find(id);
    }

    /**
    * Queries any games that contain the given string
    */
    public IEnumerable<Game> GetGamesByName(string name)
    {
        return _gameContext.Games.Where(g => g.Name.Contains(name)).ToList();
    }

    /**
    * Removes a game from the table given a game object is provided
    */
    public void DeleteGame(Game deleteGame)
    {
        _gameContext.Games.Remove(deleteGame);
        _gameContext.SaveChanges();
    }

    /**
    * Creates a new game object given a Game object is provided
    */
    public Game NewGame(Game game)
    {
        _gameContext.Games.Add(game);
        _gameContext.SaveChanges();
        return game;
    }
}