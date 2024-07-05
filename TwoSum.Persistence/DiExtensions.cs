using Enterprise.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwoSum.Application.Solutions.Contracts;
using TwoSum.Persistence.Context;
using TwoSum.Persistence.Repositories;

namespace TwoSum.Persistence;

public static class DiExtensions
{
    public static IServiceCollection AddPersistenseLayer(this IServiceCollection serviceDescriptors, IConfiguration config)
    {
        serviceDescriptors.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        string? mainConnectionString = config.GetConnectionString("PostgresEF");
        serviceDescriptors.AddDbContext<TwoSumContext>((sp, builder) =>
        {
            var intetrceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            builder.UseNpgsql(mainConnectionString, b => { b.MigrationsAssembly("TwoSum.Persistence"); })
            .AddInterceptors(intetrceptor!);
        });

        serviceDescriptors.AddScoped<ISolutionRepository, SolutionRepository>();

        return serviceDescriptors;
    }
}
