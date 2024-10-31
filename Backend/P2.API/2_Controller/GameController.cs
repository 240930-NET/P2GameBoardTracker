
namespace P2.API.Controller;
using Microsoft.AspNetCore.Mvc;
using P2.API.Service;
using P2.API.Model;
using Microsoft.AspNetCore.JsonPatch;
using P2.API.Model.DTO;

[Route("api/Game")]
[ApiController]
public class GameController : ControllerBase
{

	private readonly IGameService _gameService;
	public GameController(IGameService gameService) => _gameService = gameService;
	//should be able to get all gamnes
	[HttpGet]
	public IActionResult GetAllGames()
	{
		var games = _gameService.GetAllGames();
		return Ok(games);
	}

	//Should be able to get games by id

	[HttpGet("{id}")]
	public IActionResult GetGameById(int id)
	{
		var game = _gameService.GetGameById(id);
		if (game == null)
		{
			//return not found
			return NotFound();
		}
		return Ok(game);
	}
	//should be able to get games by name (or using LIKE for approximates)
	[HttpGet("/GetGamesByName/{name}")]
	public IActionResult GetGamesByName(string name)
	{
		var gameName = _gameService.GetGamesByName(name);
		return Ok(gameName);
	}
	//should be able to remove a game 
	[HttpDelete]
	public IActionResult RemoveGame(int id)
	{
		try
		{
			Game? game = _gameService.GetGameById(id);
			if (game != null)
			{
				//return not found
				_gameService.DeleteGame(game);
				return Ok();
			}
			return NotFound();
		}
		catch (Exception)
		{
			return BadRequest("Could not delete game");
		}
	}

	//should be able to create a game
	[HttpPost]
	public IActionResult AddNewGame([FromBody] GameDto gameDto)
	{
		try
		{
			var NewGame = _gameService.NewGame(gameDto);
			return Ok(NewGame);
		}
		catch (Exception)
		{
			return BadRequest("Could not create game");
		}
	}

    private readonly IGameService _gameService;
    public GameController(IGameService gameService) => _gameService = gameService;

    /**
    * GET endpoint that queries the game table for all games
    * Returns 200(OK) + the list of games (list is empty if no games have been added)
    */
    [HttpGet]
    public IActionResult GetAllGames()
    {
        var games = _gameService.GetAllGames();
        return Ok(games);
    }


    /**
    * GET endpoint that queries the game table for a specific entry
    * based on a game ID
    * Returns 200(OK) + the entry in the response body if it's found
    * Returns 404(NOT FOUND) otherwise
    */
    [HttpGet("{id}")]
    public IActionResult GetGameById(int id)
    {
        var game = _gameService.GetGameById(id);
        if (game == null)
        {
            //return not found
            return NotFound();
        }
        return Ok(game);
    }
    /**
    * GET endpoint that queries the backlog table for entries whose name contains the given string
    * Returns 200(OK) + a list of all the entries that match
    */
    [HttpGet("/GetGamesByName/{name}")]
    public IActionResult GetGamesByName(string name)
    {
        var gameName = _gameService.GetGamesByName(name);
        return Ok(gameName);
    }
    /**
    * DELETE endpoint that removes a game from the table
    * Returns 200(OK) if the entry has been successfully deleted
    * Returns 404(NOT FOUND) if the entry to be deleted doesn't exist
    * Returns 400(BAD REQUEST) otherwise
    */
    [HttpDelete]
    public IActionResult RemoveGame(int id)
    {
        try{
            Game? game = _gameService.GetGameById(id);
            if (game != null)
            {
                //return not found
                _gameService.DeleteGame(game);
                return Ok();
            }
            return NotFound();
        }
        catch(Exception){
            return BadRequest("Could not delete game");
        }
    }

    /**
    * POST endpoint that creates a new game entry ot the game table
    * Returns 200(OK) + the new entry in the response body if it's successfully created
    * Returns 400(BAD REQUEST) otherwise
    */
    [HttpPost]
    public IActionResult AddNewGame([FromBody] Game game)
    {
        try{
            _gameService.NewGame(game);
            return Ok(game);
        }
        catch(Exception){
            return BadRequest("Could not create game");
        }
    }


	//for now, should be able to edit a game (but if it's solely steam api, this should not be the case)








	//Body in swagger should be something like:
	//     [
	//   {
	//     "op": "replace",
	//     "path": "/username",
	//     "value": "patchedValue"
	//   }]
	// [HttpPatch("{id}")]
	// public IActionResult EditUser(int id, [FromBody] JsonPatchDocument<User> userPatch)
	// {
	//     if (userPatch != null)
	//     {
	//         User? editUser = _userService.GetUserById(id);
	//         if(editUser == null)
	//         {
	//             return NotFound();
	//         }

	//         userPatch.ApplyTo(editUser, ModelState);
	//         _userService.EditUser(editUser);
	//         if(!ModelState.IsValid)
	//         {
	//             return BadRequest(ModelState);
	//         }
	//         return Ok(editUser);
	//     }
	//     else
	//     {
	//         return BadRequest(ModelState);
	//     }
	// }
}