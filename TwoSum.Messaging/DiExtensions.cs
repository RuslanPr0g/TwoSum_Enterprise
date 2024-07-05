using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using TwoSum.Application.Solutions.NotificationHandlers;

namespace TwoSum.Messaging;

public static class DiExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddMassTransit(x =>
        {
            x.AddConsumers(typeof(SolutionDomainEventHandler).Assembly);

            x.UsingInMemory((ctx, cfg) =>
            {
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return serviceDescriptors;
    }
}
