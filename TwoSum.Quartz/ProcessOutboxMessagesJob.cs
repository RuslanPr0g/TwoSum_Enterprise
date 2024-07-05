using Enterprise.Domain;
using Enterprise.Persistence.Outbox;
using Enterprise.Quartz.Jobs;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using TwoSum.Domain.Events;
using TwoSum.Persistence.Context;

namespace TwoSum.Quartz;

public class ProcessOutboxMessagesJob : IProcessOutboxMessagesJob
{
    private readonly TwoSumContext _twoSumContext;
    private readonly IPublishEndpoint _publisher;

    public ProcessOutboxMessagesJob(TwoSumContext twoSumContext, IPublishEndpoint publisher)
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
            try
            {
                var messageType = typeof(SolutionCreatedDomainEvent).Assembly.GetType(message.Type);

                if (messageType is null)
                {
                    message.Error = $"Type '{message.Type}' could not be resolved.";
                    continue;
                }

                var domainEvent = JsonConvert.DeserializeObject(message.Content, messageType, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                });

                if (domainEvent is null)
                {
                    continue;
                }

                await _publisher.Publish(domainEvent);

                message.ProcessedOnUtc = DateTime.UtcNow;
                message.Error = string.Empty;
            }
            catch (Exception ex)
            {
                message.Error = JsonConvert.SerializeObject(ex);
                throw;
            }
        }

        await _twoSumContext.SaveChangesAsync();
    }
}
