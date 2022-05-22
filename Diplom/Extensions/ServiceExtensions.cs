using System.IO;
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
        services.AddScoped<DbContext, ApplicationDbContext>();
        services.AddScoped(typeof(BaseRepository<>));
        services.AddScoped<TagRepository>();
        services.AddScoped<TestRepository>();
        services.AddAutoMapper(typeof(Startup));
        services.AddScoped<ApplicationContext>();
        services.AddScoped<IBucket, BucketStorage>(b =>
            new BucketStorage(Path.Combine(appEnvironment.ContentRootPath, appEnvironment.WebRootPath ?? "")));
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMaterialService, MaterialService>();
        services.AddScoped<ISchoolAreaService, SchoolAreaService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IEducationalMaterialService, EducationalMaterialService>();
        services.AddScoped<ITestService, TestService>();
        return services;
    }
}