using Enterprise.Domain;
using Enterprise.Persistence.Outbox;
using Enterprise.Quartz.Jobs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using TwoSum.Persistence.Context;

namespace TwoSum.Quartz;

public class ProcessOutboxMessagesJob : IProcessOutboxMessagesJob
{
    private readonly TwoSumContext _twoSumContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(TwoSumContext twoSumContext, IPublisher publisher)
    {
        _twoSumContext = twoSumContext;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _twoSumContext
        .Set<OutboxMessage>()
        .Where(m => m.ProcessedOnUtc == null)
        .Take(20)
        .ToListAsync();

        foreach (var message in messages)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content);

            if (domainEvent is null)
            {
                continue;
            }

            await _publisher.Publish(domainEvent);

            message.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _twoSumContext.SaveChangesAsync();
    }
}
