using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using TwoSum.Application.Solutions.Contracts;
using TwoSum.Domain.Events;
using TwoSum.Messaging.Hubs;

namespace TwoSum.Application.Solutions.NotificationHandlers;

public sealed class SolutionDomainEventHandler
    : IConsumer<SolutionCreatedDomainEvent>, IConsumer<NextSolutionIterationRequested>
{
    private readonly ISolutionRepository _repository;
    private readonly Microsoft.AspNetCore.SignalR.IHubContext<SolutionHub> _hubContext;
    private readonly ILogger<SolutionDomainEventHandler> _logger;

    public SolutionDomainEventHandler(
        ISolutionRepository repository,
        Microsoft.AspNetCore.SignalR.IHubContext<SolutionHub> hubContext,
        ILogger<SolutionDomainEventHandler> logger)
    {
        _repository = repository;
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SolutionCreatedDomainEvent> context)
    {
        using (_logger.BeginScope(new { context.CorrelationId }))
        {
            _logger.LogInformation("Starting to process event {event}.", typeof(SolutionCreatedDomainEvent));

            var solution = await _repository.GetSolutionById(context.Message.SolutionId);

            if (solution is null || solution.IsSolved())
            {
                return;
            }

            solution.MoveNext();
            await _repository.SaveChanges();

            _logger.LogInformation("Finished processing event {event}.", typeof(SolutionCreatedDomainEvent));
        }
    }

    public async Task Consume(ConsumeContext<NextSolutionIterationRequested> context)
    {
        using (_logger.BeginScope(new { context.CorrelationId }))
        {
            _logger.LogInformation("Starting to process event {event}.", typeof(SolutionCreatedDomainEvent));

            var solution = await _repository.GetSolutionById(context.Message.SolutionId);

            if (solution is null || solution.IsSolved())
            {
                return;
            }

            solution.MoveNext();
            await _repository.SaveChanges();

            var res = solution.RetrieveSolutionAsString();

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", res);

            _logger.LogInformation("Finished processing event {event}.", typeof(SolutionCreatedDomainEvent));
        }
    }
}
