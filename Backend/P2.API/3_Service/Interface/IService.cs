using P2.API.Model;
using P2.API.Model.DTO;

namespace P2.API.Service;

public interface IUserService
{
	IEnumerable<User> GetAllUsers();
	public User? GetUserById(int id);
	public User NewUser(UserDto user);
	public User? GetUserByUsername(string username);
	public void DeleteUser(User deleteUser);
	public void EditUser(User user);

	public bool VerifyPassword(string password, string passwordHash);
	public User? AuthenticateUser(string username, string password);
	void UpdatePassword(User user, string newPassword);
}
public interface IGameService
{
	IEnumerable<Game> GetAllGames();
	public Game? GetGameById(int id);

	public IEnumerable<Game> GetGamesByName(string name);
	public void DeleteGame(Game deleteGame);
	public Game NewGame(GameDto gameDto);
}

public interface IBacklogService{

    public Object? GetBacklogEntry(int id, int gameId);
    public IEnumerable<object> GetBacklogByUserId(int id);
    public void DeleteGameFromUserBacklog(int id, int gameId);
    public Backlog? AddGameToBacklog(Backlog log);


}

public interface IIGDBService{
	public List<Game> GetGamesFiltered(string name, List<Game> excludeGames, List<int>? genres = null, List<int>? platforms = null, int limit = 5);
}