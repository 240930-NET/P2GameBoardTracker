using P2.API.Service;
using P2.API.Data;
using P2.API.Model;
using P2.API.Repository;
using Moq;
using Microsoft.IdentityModel.Tokens;
namespace P2.TEST;

public class BacklogServiceTest
{
    private Mock<IBacklogRepository> mockRepo;
    private BacklogService  backlogService;
    public BacklogServiceTest()
    {
        mockRepo = new();
        backlogService = new(mockRepo.Object);
    }

    [Fact]
    public void GetExistingBacklogEntry()
    {

        List<Backlog> backlogs = [new Backlog{UserId = 1, GameId = 1, Completed = false},
        new Backlog{UserId = 1, GameId = 2, Completed = true}];

        mockRepo.Setup(repo => repo.GetBacklogEntry(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(backlogs.FirstOrDefault(bl => bl.UserId == 1 && bl.GameId == 1));

        //Act
        var result = backlogService.GetBacklogEntry(1, 1);
        Assert.Equal(result, backlogs[0]);
    }
    [Fact]
    public void GetFakeBacklogEntry()
    {

        List<Backlog> backlogs = [new Backlog{UserId = 1, GameId = 1, Completed = false},
        new Backlog{UserId = 1, GameId = 2, Completed = true}];

        mockRepo.Setup(repo => repo.GetBacklogEntry(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(backlogs.FirstOrDefault(bl => bl.UserId == 3 && bl.GameId == 3));

        //Act
        //Assert
        Assert.Null(backlogService.GetBacklogEntry(3, 3));
    }
    [Fact]
    public void GetBacklogEntryById()
    {

        List<Backlog> backlogs = [new Backlog{UserId = 1, GameId = 1, Completed = false},
        new Backlog{UserId = 1, GameId = 2, Completed = true}, 
        new Backlog{UserId = 2, GameId = 2, Completed = true}];
        List<Backlog> backlogsUser1 = [new Backlog{UserId = 1, GameId = 1, Completed = false},
        new Backlog{UserId = 1, GameId = 2, Completed = true}];

        mockRepo.Setup(repo => repo.GetBacklogByUserId(It.IsAny<int>()))
            .Returns(backlogsUser1);

        var result = backlogService.GetBacklogByUserId(1);
        Assert.Equal(result, backlogsUser1);
    }

    [Fact]
    public void NewBacklogEntrySuccessful()
    {
        List<Backlog> backlogs = [];
        Backlog bl = new Backlog{UserId = 1, GameId = 1, Completed = false};
        mockRepo.Setup(repo => repo.AddGameToBacklog(It.IsAny<Backlog>()))
            .Callback(() => backlogs.Add(bl))
            .Returns(bl);
        var result = backlogService.AddGameToBacklog(bl);

        Assert.False(backlogs.IsNullOrEmpty());
        Assert.Equal(result.UserId, 1);
        Assert.Equal(result.GameId, 1);
    }
    [Fact]
    public void DeleteBacklogEntry()
    {
        //TODO:
        List<Backlog> backlogs = [new Backlog{UserId = 1, GameId = 1, Completed = false},
        new Backlog{UserId = 1, GameId = 2, Completed = true}];
        mockRepo.Setup(repo => repo.DeleteGameFromUserBacklog(It.IsAny<int>(), It.IsAny<int>()));
        backlogService.DeleteGameFromUserBacklog(1, 1);
        // Assert.DoesNotContain(gameList, u => u.gameName.Equals("deleteThis"));
    }
}
