using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Models.Application;

public class ApplicationUserManager : UserManager<ApplicationUser>
{
    public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
    }

    public ApplicationUser GetUser(string id)
    {
        return Users.FirstOrDefault(u => u.Id == id);
    }

    private ApplicationUser GetUser(string id, Func<IQueryable<ApplicationUser>, IQueryable<ApplicationUser>> includes)
    {
        return includes(Users).FirstOrDefault(u => u.Id == id);
    }

    public ApplicationUser GetUser(ClaimsPrincipal principal)
    {
        return GetUser(principal, null);
    }
    
    private ApplicationUser GetUser(ClaimsPrincipal principal, Func<IQueryable<ApplicationUser>, IQueryable<ApplicationUser>> includes)
    {
        var id = GetUserId(principal);
        return GetUser(id, includes);
    }

    public ApplicationUser GetUserWithComponents(string id)
    {
        return GetUser(id, IncludesComponents);
    }
    
    public ApplicationUser GetUserWithComponents(ClaimsPrincipal principal)
    {
        return GetUser(principal, IncludesComponents);
    }

    private IQueryable<ApplicationUser> IncludesComponents(IQueryable<ApplicationUser> arg)
    {
        return arg.Include(u => u.Activities)
            .Include(u => u.Materials);
    }
}