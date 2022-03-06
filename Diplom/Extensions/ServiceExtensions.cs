using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services.Realizations;
using Microsoft.Extensions.DependencyInjection;

namespace Diplom.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Startup));
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<IBucket, BucketStorage>();
        return services;
    }
}