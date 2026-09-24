using JobService.Application.Abstractions;
using JobService.Domain;

namespace JobService.Infrastructure.RecurringJobs;

public sealed class AutoCloseOldJobsJob
{
    private readonly IJobRepository _repository;

    public AutoCloseOldJobsJob(IJobRepository repository) => _repository = repository;

    public void Run()
    {
        var cutoff = DateTime.UtcNow.AddDays(-7);

        foreach (var job in _repository.GetAll())
        {
            if (job.Status == JobStatus.Open && job.CreatedAtUtc < cutoff)
            {
                job.Close(DateTime.UtcNow);
                _repository.Save(job);
            }
        }
    }
}
