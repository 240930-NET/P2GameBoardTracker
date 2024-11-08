namespace P2.API.Repository;
using P2.API.Model;
using P2.API.Data;
using Microsoft.EntityFrameworkCore;

public class BacklogRepository : IBacklogRepository
{
    private readonly BacklogContext _backlogContext;

    public BacklogRepository(BacklogContext backlogContext) => _backlogContext = backlogContext;


    /**
    * Finds backlog entry by userid + gameid.
    * Updated so that it joins the backlog entry with the game entry as well
    * for further details
    */
    public Object? GetBacklogEntry(int id, int gameId)
    {
        return _backlogContext.Backlogs.Join(_backlogContext.Games, backlog => backlog.GameId, game => game.GameId, 
            (backlog, game) => new
            {
                //whatever new details added for the backlog and game should be added here as well
                GameId = game.GameId,
                UserId = backlog.UserId,
                Name = game.Name,
                Description = game.Description,
                ImageURL = game.ImageURL,
                Rating = game.Rating, 
                completionStatus = backlog.Completed,
                completionDate = backlog.CompletionDate
            }).Where(x => x.GameId == gameId && x.UserId == id).FirstOrDefault();
    }
    /**
    * Finds a user's backlog using user id
    * Joins backlog table and game table to return a list that include the game information plus
    * the information specific to the user's backlog regarding the game 
    */
    public IEnumerable<object> GetBacklogByUserId(int id)
    {
        // return _backlogContext.Backlogs
        // .Where(bl => bl.UserId == id)
        // .ToList();
        return _backlogContext.Backlogs.Join(_backlogContext.Games, backlog => backlog.GameId, game => game.GameId, 
            (backlog, game) => new
            {
                //whatever new details added for the backlog and game should be added here as well
                GameId = game.GameId,
                Name = game.Name,
                Description = game.Description,
                ImageURL = game.ImageURL,
                Rating = game.Rating, 
                completionStatus = backlog.Completed,
                completionDate = backlog.CompletionDate
            }).ToList();
    }
    /**
    * Deletes backlog entry given a backlog object given by the service
    */
    public void DeleteGameFromUserBacklog(int id, int gameId)
    {
        Backlog? log = _backlogContext.Backlogs.Find(id, gameId);
        //could just find it at the beginning instead?
        _backlogContext.Backlogs.Remove(log);
        _backlogContext.SaveChanges();
    }
    /**
    * Adds a new entry to the backlog table given a backlog object by the service
    */
    public Backlog? AddGameToBacklog(Backlog log)
    {
        //also try and figure out a better way to just return the game itself instead?
        _backlogContext.Backlogs.Add(log);
        _backlogContext.SaveChanges();
        return log;
    }
    

    


}