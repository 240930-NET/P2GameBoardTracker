
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
	[HttpGet("/GetGamesByName/test/{name}")]
	public IActionResult GetGamesByNameTest(string name)
	{
		var games = _gameService.TestApi(name);
		return Ok(games);
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