# Changes I made 

## GameController.cs 

### On Swagger 
1. #### Original receiving JSON (Post Method GameController.cs)
```json
{
  "gameId": 0,
  "name": "string",
  "description": "string"
}
```

   - **Original conflict**: The user shouldn't be obligated to insert an ID.
   - Can get 404 body errors easily if the user tries to insert a subsequent ID.
   - ID is set up to auto-increment regardless.

2. #### Created GameDto.cs
   - To prevent this, I made Dto to negate the gameId as it's not needed for the Post method in GameController.

```csharp 
namespace P2.API.Model.DTO;

public class GameDto
{
    public required string Name { get; set; }
    public string Description { get; set; } = "";
}
```

## Now update respective files

### IService.cs 

#### 1. Modify the NewGame method in <span style="color:yellow">IGameService</span> to receive GameDto

```csharp 
public interface IGameService
{
    IEnumerable<Game> GetAllGames();
    public Game? GetGameById(int id);
    public IEnumerable<Game> GetGamesByName(string name);
    public void DeleteGame(Game deleteGame);
    public Game NewGame(GameDto gameDto);
}
```

### 2. Implement this method with the new GameDto in <span style="color:yellow">GameService.cs</span> 

```csharp 
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

    public GameService(IGameRepository gameRepository, IMapper mapper)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
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
        return _gameRepository.GetGamesByName(name);
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
```

### Changes 
1. ```csharp
   private readonly IMapper _mapper;
   ```
   - Added Dependency Injection for AutoMapper to map `Game` to `GameDto`.

2. Update constructor 
 ```csharp
   public GameService(IGameRepository gameRepository, IMapper mapper)
   {
       _gameRepository = gameRepository;
       _mapper = mapper;
   }
   ```
3. Update NewGame method to receive GameDto 
```csharp 
public Game NewGame(GameDto gameDto)
{
    if (string.IsNullOrEmpty(gameDto.Name))
    {
        throw new Exception("Invalid game! Please input at least a valid game name");
    }
    var game = _mapper.Map<Game>(gameDto);
    return _gameRepository.NewGame(game);
}
```

### Explanation:
- **Input Validation**: Checks if `Name` is null or empty; throws an exception if invalid.
- **Object Mapping**: Uses `_mapper.Map<Game>(gameDto)` to convert `GameDto` to `Game`, mapping properties and handling type conversions.
- **Repository Call**: Saves the new `Game` object with `_gameRepository.NewGame(game)`.
- **Return**: Returns the newly created `Game` object, now with its ID set.

### Update GameController.cs AddNewGame method 
```csharp
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
```

### Explanation:
- **Input**: Accepts a `GameDto` object from the request body.
- **Service Call**: Invokes `_gameService.NewGame(gameDto)` to create a new game.
- **Success Response**: Returns HTTP 200 OK with the newly created game object.
- **Error Handling**: Catches any exceptions and returns HTTP 400 Bad Request with an error message.
- **Benefit**: Simplifies API input, handles errors gracefully, and returns the created game to the client.