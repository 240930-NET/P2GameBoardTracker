
namespace P2.API.Controller;
using Microsoft.AspNetCore.Mvc;
using P2.API.Service;
using P2.API.Model;
using Microsoft.AspNetCore.JsonPatch;

[Route("api/Backlog")]
[ApiController]
public class BacklogController : ControllerBase
{
    private readonly IBacklogService _backlogService;
    public BacklogController(IBacklogService backlogService) => _backlogService = backlogService;
    /**
    * GET endpoint that queries the backlog table for a specific entry
    * based on a game ID and user ID
    * Returns 200(OK) + the entry in the response body if it's found
    * Returns 404(NOT FOUND) otherwise
    */
    [HttpGet("GetBacklogEntry")]
    public IActionResult GetBacklogEntry([FromQuery] int userId, [FromQuery] int gameId)
    {
        Object? entry = _backlogService.GetBacklogEntry(userId, gameId);
        if(entry != null)
        {
            return Ok(entry);
        }
        return NotFound();
    }
    /**
    * GET endpoint that queries the backlog table for all the games on a user's backlog
    * Returns 200(OK) + a list of all the games in the backlog
    * List is empty if no games are in the backlog
    */
    [HttpGet("{userid}")]
    public IActionResult GetUsersBacklog(int userid)
    {
        return Ok(_backlogService.GetBacklogByUserId(userid));
    }
    /**
    * DELETE endpoint that checks if a specific entry exists in a user's backlog
    * and if it does, removes it from the user's backlog.
    * Returns 200(OK) if the entry was successfully removed
    * Returns 404(NOT FOUND) if the entry doesn't exist
    * Returns 400(BAD REQUEST) otherwise
    */
    [HttpDelete("DeleteEntryFromUser")]
    public IActionResult DeleteEntryFromUser([FromQuery] int userId, [FromQuery] int gameId)
    {
        try 
        {
            Object? bl = _backlogService.GetBacklogEntry(userId, gameId);
            if(bl != null)
            {
                _backlogService.DeleteGameFromUserBacklog(userId, gameId);
                return Ok();
            }
            return NotFound();
        }
        catch(Exception)
        {
            return BadRequest("Could not delete entry from backlog");
        }
        
    }

    /**
    * POST endpoint that creates a new backlog entry given a backlog object
    * Returns 200(OK) + the new entry in the response body if it was successfully created
    * Returns 400(BAD REQUEST) otherwise
    */
    [HttpPost]
    public IActionResult AddGameToBacklog(Backlog backlog)
    {
        try{
            
            return Ok(_backlogService.AddGameToBacklog(backlog));
        }
        catch(Exception){
            return BadRequest("Could not add entry to backlog");
        }
    }
}