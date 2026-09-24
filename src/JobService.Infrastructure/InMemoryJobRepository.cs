using System.Collections.Concurrent;
using JobService.Application.Abstractions;
using JobService.Domain;

namespace JobService.Infrastructure;

public sealed class InMemoryJobRepository : IJobRepository
{
    private readonly ConcurrentDictionary<Guid, Job> _jobs = new();

    public IReadOnlyList<Job> GetAll() => _jobs.Values.OrderByDescending(x => x.CreatedAtUtc).ToList();

    public Job? GetById(Guid id) => _jobs.TryGetValue(id, out var job) ? job : null;

    public void Add(Job job) => _jobs[job.Id] = job;

    public void Save(Job job) => _jobs[job.Id] = job;
}
