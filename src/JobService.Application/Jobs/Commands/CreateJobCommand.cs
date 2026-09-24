using JobService.Application.Abstractions;
using JobService.Domain;

namespace JobService.Application.Jobs.Commands;

public sealed record CreateJobCommand(string Title) : IRequest<Job>;

public sealed class CreateJobHandler : IRequestHandler<CreateJobCommand, Job>
{
    private readonly IJobRepository _repository;

    public CreateJobHandler(IJobRepository repository) => _repository = repository;

    public Job Handle(CreateJobCommand request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("Title is required.");

        var job = new Job(Guid.NewGuid(), request.Title.Trim(), DateTime.UtcNow);
        _repository.Add(job);
        return job;
    }
}
