
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
    //get specific backlog entry
    [HttpGet("GetBacklogEntry")]
    public IActionResult GetBacklogEntry([FromQuery] int userId, [FromQuery] int gameId)
    {
        Backlog? entry = _backlogService.GetBacklogEntry(userId, gameId);
        if(entry != null)
        {
            return Ok(entry);
        }
        return NotFound();
    }
    //should be able to get a users backlog
    [HttpGet("{userid}")]
    public IActionResult GetUsersBacklog(int userid)
    {
        return Ok(_backlogService.GetBacklogByUserId(userid));
    }
    //should be able to delete a game from a specific user's backlog
    [HttpDelete("DeleteEntryFromUser")]
    public IActionResult DeleteEntryFromUser([FromQuery] int userId, [FromQuery] int gameId)
    {
        try 
        {
            Backlog? bl = _backlogService.GetBacklogEntry(userId, gameId);
            if(bl != null)
            {
                _backlogService.DeleteGameFromUserBacklog(bl);
                return Ok();
            }
            return NotFound();
        }
        catch(Exception)
        {
            return BadRequest("Could not delete entry from backlog");
        }
        
    }

    //should be able to add a new game to a user's backlog
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