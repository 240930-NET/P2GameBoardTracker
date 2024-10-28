namespace P2.API.Repository;
using P2.API.Model;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();
    public User? GetUserById(int id);
    public User NewUser(User user);
    public User? GetUserByUsername(string username);
    public void DeleteUser(User deleteUser);
    public void EditUser(User user);
}
public interface IBacklogRepository
{
    public Backlog? GetBacklogEntry(int id, int gameId);
    public IEnumerable<Backlog> GetBacklogByUserId(int id);
    public void DeleteGameFromUserBacklog(Backlog log);
    public Backlog? AddGameToBacklog(Backlog log);
}
public interface IGameRepository
{
    // IEnumerable<User> GetAllGames();
    IEnumerable<Game> GetAllGames();
    public Game? GetGameById(int id);
    public IEnumerable<Game> GetGamesByName(string name);
    public void DeleteGame(Game deleteGame);
    public Game NewGame(Game game);
}
