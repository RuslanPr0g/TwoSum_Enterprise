using Enterprise.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using TwoSum.Persistence.Context;

namespace TwoSum.Quartz;

public class CleanOutboxJob : IJob
{
    private readonly TwoSumContext _twoSumContext;

    public CleanOutboxJob(TwoSumContext twoSumContext)
    {
        _twoSumContext = twoSumContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _twoSumContext
        .Set<OutboxMessage>()
        .Where(m => m.ProcessedOnUtc != null)
        .Take(5)
        .ToListAsync();

        foreach (var message in messages)
        {
            try
            {
                _twoSumContext.Remove(message);
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
