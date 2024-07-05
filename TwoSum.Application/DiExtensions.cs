using Enterprise.Application.Generators;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TwoSum.Application.Solutions.Contracts;
using TwoSum.Application.Solutions.Services;

namespace TwoSum.Application;

public static class DiExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceDescriptors)
    {
        serviceDescriptors.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        serviceDescriptors.AddSingleton<IIdGenerator<Guid>, GuidIdGenerator>();

        serviceDescriptors.AddScoped<ISolutionService, SolutionService>();

        return serviceDescriptors;
    }
}
