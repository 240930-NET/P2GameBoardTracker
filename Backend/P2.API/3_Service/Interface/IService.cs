using P2.API.Model;

namespace P2.API.Service;

public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    public User? GetUserById(int id);
    public User NewUser(User user);
    public User? GetUserByUsername(string username);
    public void DeleteUser(User deleteUser);
    public void EditUser(User user);
}
public interface IGameService
{
    IEnumerable<Game> GetAllGames();
    public Game? GetGameById(int id);

    public IEnumerable<Game> GetGamesByName(string name);
    public void DeleteGame(Game deleteGame);
    public Game NewGame(Game game);
}

public interface IBacklogService{
    public Object? GetBacklogEntry(int id, int gameId);
    public IEnumerable<object> GetBacklogByUserId(int id);
    public void DeleteGameFromUserBacklog(int id, int gameId);
    public Backlog? AddGameToBacklog(Backlog log);

}