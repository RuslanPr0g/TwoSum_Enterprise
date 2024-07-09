using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace TwoSum.Quartz;

public static class DiExtensions
{
    public static IServiceCollection AddJobs(this IServiceCollection serviceDescriptors, IConfiguration config)
    {
        serviceDescriptors.AddQuartz(conf =>
        {
            var outboxKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            var clearanceKey = new JobKey(nameof(CleanOutboxJob));

            conf.AddJob<ProcessOutboxMessagesJob>(outboxKey).AddTrigger(trigger =>
            {
                trigger.ForJob(outboxKey).WithSimpleSchedule(schedule =>
                    schedule.WithIntervalInSeconds(20).RepeatForever());
            });

            conf.AddJob<CleanOutboxJob>(clearanceKey).AddTrigger(trigger =>
            {
                trigger.ForJob(clearanceKey).WithSimpleSchedule(schedule =>
                    schedule.WithIntervalInSeconds(60).RepeatForever());
            });
        });

        serviceDescriptors.AddQuartzHostedService();

        return serviceDescriptors;
    }
}