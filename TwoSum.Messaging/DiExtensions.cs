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
            x.SetKebabCaseEndpointNameFormatter();
            x.SetInMemorySagaRepositoryProvider();

            var asm = typeof(SolutionDomainEventHandler).Assembly;

            x.AddConsumers(asm);
            x.AddSagaStateMachines(asm);
            x.AddSagas(asm);
            x.AddActivities(asm);

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return serviceDescriptors;
    }
}
