using JobService.Domain;

namespace JobService.Application.Abstractions;

public interface IJobRepository
{
    IReadOnlyList<Job> GetAll();
    Job? GetById(Guid id);
    void Add(Job job);
    void Save(Job job);
}
