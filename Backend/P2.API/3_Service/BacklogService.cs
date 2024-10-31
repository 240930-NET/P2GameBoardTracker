using P2.API.Model;
using P2.API.Repository;

namespace P2.API.Service;

public class BacklogService : IBacklogService
{
    private readonly IBacklogRepository _backlogRepository;

    public BacklogService(IBacklogRepository backlogRepository) => _backlogRepository = backlogRepository;
    public IEnumerable<object> GetBacklogByUserId(int id)
    {
        return _backlogRepository.GetBacklogByUserId(id);
    }
    public void DeleteGameFromUserBacklog(int id, int gameId)
    {
        _backlogRepository.DeleteGameFromUserBacklog(id, gameId);
    }
    public Backlog? AddGameToBacklog(Backlog log)
    {
        return _backlogRepository.AddGameToBacklog(log);
    }

    public Object? GetBacklogEntry(int id, int gameId)
    {
        return _backlogRepository.GetBacklogEntry(id, gameId);
    }
}