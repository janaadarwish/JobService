using JobService.Application.Abstractions;
using JobService.Domain;

namespace JobService.Application.Jobs.Commands;

public sealed record CloseJobCommand(Guid Id) : IRequest<bool>;

public sealed class CloseJobHandler : IRequestHandler<CloseJobCommand, bool>
{
    private readonly IJobRepository _repository;

    public CloseJobHandler(IJobRepository repository) => _repository = repository;

    public bool Handle(CloseJobCommand request)
    {
        var job = _repository.GetById(request.Id);
        if (job is null) return false;

        job.Close(DateTime.UtcNow);
        _repository.Save(job);
        return true;
    }
}
