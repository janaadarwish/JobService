using JobService.Application.Abstractions;
using JobService.Domain;

namespace JobService.Application.Jobs.Queries;

public sealed record GetJobsQuery : IRequest<IReadOnlyList<Job>>;

public sealed class GetJobsHandler : IRequestHandler<GetJobsQuery, IReadOnlyList<Job>>
{
    private readonly IJobRepository _repository;

    public GetJobsHandler(IJobRepository repository) => _repository = repository;

    public IReadOnlyList<Job> Handle(GetJobsQuery request) => _repository.GetAll();
}
