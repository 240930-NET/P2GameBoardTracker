namespace P2.API.Repository;
using P2.API.Model;
using P2.API.Data;
using Microsoft.EntityFrameworkCore;

public class BacklogRepository : IBacklogRepository
{
    private readonly BacklogContext _backlogContext;

    public BacklogRepository(BacklogContext backlogContext) => _backlogContext = backlogContext;


    //get specific backlog entry
    public Backlog? GetBacklogEntry(int id, int gameId)
    {
        return _backlogContext.Backlogs.Find(id, gameId);
    }
    //should be able to get a users backlog
    public IEnumerable<Backlog> GetBacklogByUserId(int id)
    {
        return _backlogContext.Backlogs
        .Where(bl => bl.UserId == id)
        .ToList();
        //should figure out a way to display them as games maybe?
    }
    //should be able to delete a game from a specific user's backlog
    public void DeleteGameFromUserBacklog(Backlog log)
    {
        //could just find it at the beginning instead?
        _backlogContext.Backlogs.Remove(log);
        _backlogContext.SaveChanges();
    }
    //should be able to add a new game to a user's backlog
    public Backlog? AddGameToBacklog(Backlog log)
    {
        //also try and figure out a better way to just return the game itself instead?
        _backlogContext.Backlogs.Add(log);
        _backlogContext.SaveChanges();
        return log;
    }
    

    


}