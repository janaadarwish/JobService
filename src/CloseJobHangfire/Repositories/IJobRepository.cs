using CloseJobHangfire.Models;

namespace CloseJobHangfire.Repositories;

public interface IJobRepository
{
    IReadOnlyCollection<Job> GetAll();
    Job? GetById(int id);
    Job Add(string title);
    int CloseStaleJobs(DateTime cutoffUtc);
}
