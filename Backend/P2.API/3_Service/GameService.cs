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
	private readonly IGDBService _igdbService;

	public GameService(IGameRepository gameRepository, IMapper
	mapper, IGDBService igdbService)
	{
		_gameRepository = gameRepository;

		_mapper = mapper;
		_igdbService = igdbService;

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
		List<Game> games = _gameRepository.GetGamesByName(name).ToList();
		if(games.Count == 0)
		{
			//if we don't have it saved:
			//fetch from API
			games = _igdbService.GetGamesFiltered(name);
			//save to our database:
			foreach(Game game in games)
			{
				_gameRepository.NewGame(game);
			}
		}
		//return games either way
		return games;
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