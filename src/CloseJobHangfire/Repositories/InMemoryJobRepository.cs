using CloseJobHangfire.Models;

namespace CloseJobHangfire.Repositories;

public sealed class InMemoryJobRepository : IJobRepository
{
    private readonly object _lock = new();
    private readonly List<Job> _jobs = new();
    private int _nextId = 1;

    public InMemoryJobRepository()
    {
        AddSeed("Prepare interview questions", DateTime.UtcNow.AddDays(-10));
        AddSeed("Review candidate applications", DateTime.UtcNow.AddDays(-2));
    }

    public IReadOnlyCollection<Job> GetAll()
    {
        lock (_lock)
        {
            return _jobs.Select(Clone).ToList().AsReadOnly();
        }
    }

    public Job? GetById(int id)
    {
        lock (_lock)
        {
            var job = _jobs.FirstOrDefault(x => x.Id == id);
            return job is null ? null : Clone(job);
        }
    }

    public Job Add(string title)
    {
        lock (_lock)
        {
            var job = new Job
            {
                Id = _nextId++,
                Title = title,
                Status = JobStatus.Open,
                CreatedAtUtc = DateTime.UtcNow
            };

            _jobs.Add(job);
            return Clone(job);
        }
    }

    public int CloseStaleJobs(DateTime cutoffUtc)
    {
        lock (_lock)
        {
            var staleJobs = _jobs.Where(x => x.Status == JobStatus.Open && x.CreatedAtUtc < cutoffUtc).ToList();

            foreach (var job in staleJobs)
            {
                job.Status = JobStatus.Closed;
                job.ClosedAtUtc = DateTime.UtcNow;
            }

            return staleJobs.Count;
        }
    }

    private void AddSeed(string title, DateTime createdAtUtc)
    {
        _jobs.Add(new Job
        {
            Id = _nextId++,
            Title = title,
            Status = JobStatus.Open,
            CreatedAtUtc = createdAtUtc
        });
    }

    private static Job Clone(Job job)
    {
        return new Job
        {
            Id = job.Id,
            Title = job.Title,
            Status = job.Status,
            CreatedAtUtc = job.CreatedAtUtc,
            ClosedAtUtc = job.ClosedAtUtc
        };
    }
}
