using MediatR;
using TwoSum.Application.Contracts;
using TwoSum.Domain.Events;

namespace TwoSum.Application.NotificationHandlers;

public sealed class SolutionDomainEventHandler 
    : INotificationHandler<SolutionCreatedDomainEvent>, INotificationHandler<NextSolutionIterationRequested>
{
    private readonly ISolutionRepository _repository;

    public SolutionDomainEventHandler(ISolutionRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(SolutionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var solution = await _repository.GetSolutionById(notification.SolutionId);

        if (solution is null)
        {
            return;
        }

        solution.MoveNext();
    }

    public async Task Handle(NextSolutionIterationRequested notification, CancellationToken cancellationToken)
    {
        var solution = await _repository.GetSolutionById(notification.SolutionId);

        if (solution is null)
        {
            return;
        }

        solution.MoveNext();
    }
}
