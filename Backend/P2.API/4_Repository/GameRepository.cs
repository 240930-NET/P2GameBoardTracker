namespace P2.API.Repository;
using P2.API.Model;
using P2.API.Data;

public class GameRepository : IGameRepository
{
    private readonly BacklogContext _gameContext;

    public GameRepository(BacklogContext gameContext) => _gameContext = gameContext;
    //should be able to get all gamnes
    public IEnumerable<Game> GetAllGames()
    {
        return _gameContext.Games.ToList();
    }

    //Should be able to get games by id
    public Game? GetGameById(int id)
    {
        return _gameContext.Games.Find(id);
    }

    //should be able to get games by name (or using LIKE for approximates)
    public IEnumerable<Game> GetGamesByName(string name)
    {
        return _gameContext.Games.Where(g => g.Name.Contains(name)).ToList();
    }

    //should be able to remove a game 
    public void DeleteGame(Game deleteGame)
    {
        _gameContext.Games.Remove(deleteGame);
        _gameContext.SaveChanges();
    }

    //for now, should be able to edit a game (but if it's solely steam api, this should not be the case)

    //should be able to create a game
    public Game NewGame(Game game)
    {
        _gameContext.Games.Add(game);
        _gameContext.SaveChanges();
        return game;
    }
}