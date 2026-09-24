namespace JobService.Application.Abstractions;

public interface IRequest<TResult> { }

public interface IRequestHandler<in TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    TResult Handle(TRequest request);
}
