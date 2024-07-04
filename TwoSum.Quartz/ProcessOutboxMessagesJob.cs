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
            try
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, new JsonSerializerSettings()
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
            }
        }

        await _twoSumContext.SaveChangesAsync();
    }
}
