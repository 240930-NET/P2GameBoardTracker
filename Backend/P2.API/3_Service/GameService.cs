using Microsoft.IdentityModel.Tokens;
using P2.API.Model;
using P2.API.Repository;

using P2.API.Model.DTO;
using AutoMapper;


namespace P2.API.Service;

public class GameService : IGameService
{
	private readonly IGameRepository _gameRepository;
	private readonly IMapper _mapper;

	public GameService(IGameRepository gameRepository, IMapper
	mapper)
	{
		_gameRepository = gameRepository;

		_mapper = mapper;

	}
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
	public Game NewGame(GameDto gameDto)
	{
		if (string.IsNullOrEmpty(gameDto.Name))
		{
			throw new Exception("Invalid game! Please input at least a valid game name");
		}
		var game = _mapper.Map<Game>(gameDto);
		return _gameRepository.NewGame(game);
	}
}