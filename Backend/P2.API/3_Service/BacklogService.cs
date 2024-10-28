using P2.API.Model;
using P2.API.Repository;

namespace P2.API.Service;

public class BacklogService : IBacklogService
{
    private readonly IBacklogRepository _backlogRepository;

    public BacklogService(IBacklogRepository backlogRepository) => _backlogRepository = backlogRepository;
    public IEnumerable<Backlog> GetBacklogByUserId(int id)
    {
        return _backlogRepository.GetBacklogByUserId(id);
    }
    public void DeleteGameFromUserBacklog(Backlog log)
    {
        _backlogRepository.DeleteGameFromUserBacklog(log);
    }
    public Backlog? AddGameToBacklog(Backlog log)
    {
        return _backlogRepository.AddGameToBacklog(log);
    }

    public Backlog? GetBacklogEntry(int id, int gameId)
    {
        return _backlogRepository.GetBacklogEntry(id, gameId);
    }
}