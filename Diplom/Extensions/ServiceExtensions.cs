using Infrastructure;
using Infrastructure.Models.Application;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services.Realizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Diplom.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Startup));
        services.AddScoped<DbContext, ApplicationDbContext>();
        services.AddScoped<ApplicationContext>();
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<IBucket, BucketStorage>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}