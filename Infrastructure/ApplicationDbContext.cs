using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using Infrastructure.Models;
using Infrastructure.Models.Application;
using Infrastructure.Models.Test;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    public DbSet<Activity> Activities { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<UserResult> Results { get; set; }
    public DbSet<SchoolArea> SchoolAreas { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<EducationalMaterial> EducationalMaterials { get; set; }
}