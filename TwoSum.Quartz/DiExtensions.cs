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
            var jobKet = new JobKey(nameof(ProcessOutboxMessagesJob));

            conf.AddJob<ProcessOutboxMessagesJob>(jobKet).AddTrigger(trigger =>
            {
                trigger.ForJob(jobKet).WithSimpleSchedule(schedule =>
                    schedule.WithIntervalInSeconds(20).RepeatForever());
            });
        });

        serviceDescriptors.AddQuartzHostedService();

        return serviceDescriptors;
    }
}