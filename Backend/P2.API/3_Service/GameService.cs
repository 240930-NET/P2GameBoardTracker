using Microsoft.IdentityModel.Tokens;
using P2.API.Model;
using P2.API.Repository;

namespace P2.API.Service;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository) => _gameRepository = gameRepository;

    public IEnumerable<Game> GetAllGames()
    {
        return _gameRepository.GetAllGames();
    }

    public Game? GetGameById(int id)
    {
        return _gameRepository.GetGameById(id);
    }

    public IEnumerable<Game> GetGamesByName(string name)
    {
        return _gameRepository.GetGamesByName(name);
    }
    public void DeleteGame(Game deleteGame)
    {
        _gameRepository.DeleteGame(deleteGame);
    }
    public Game NewGame(Game game)
    {
        if(game.Name.IsNullOrEmpty())
        {
            throw new Exception("Invalid game! Please input at least a valid game name");
        }
        return _gameRepository.NewGame(game);
    }
}