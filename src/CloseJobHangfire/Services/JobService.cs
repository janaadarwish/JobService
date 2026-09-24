using CloseJobHangfire.Models;
using CloseJobHangfire.Repositories;

namespace CloseJobHangfire.Services;

public sealed class JobService
{
    private static readonly TimeSpan StaleAfter = TimeSpan.FromDays(7);
    private readonly IJobRepository _repository;

    public JobService(IJobRepository repository)
    {
        _repository = repository;
    }

    public IReadOnlyCollection<Job> GetAll()
    {
        return _repository.GetAll();
    }

    public Job? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public Job Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.");
        }

        return _repository.Add(title.Trim());
    }

    public int AutoCloseStaleJobs()
    {
        var cutoffUtc = DateTime.UtcNow.Subtract(StaleAfter);
        return _repository.CloseStaleJobs(cutoffUtc);
    }
}
