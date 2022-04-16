using Infrastructure;
using Infrastructure.Models.Application;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services.Realizations;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Diplom.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IWebHostEnvironment appEnvironment)
    {
        services.AddAutoMapper(typeof(Startup));
        services.AddScoped<DbContext, ApplicationDbContext>();
        services.AddScoped<ApplicationContext>();
        services.AddScoped<IBucket, BucketStorage>(b => new BucketStorage(appEnvironment.WebRootPath));
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<TagRepository>();
        services.AddScoped<IMaterialService, MaterialService>();
        return services;
    }
}