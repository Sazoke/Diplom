using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models.Application;

public class ApplicationContext
{
    public ApplicationContext(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
    {
        CurrentUserId = userManager.GetUserId(httpContextAccessor.HttpContext?.User);
    }
    
    public string CurrentUserId { get; set; }
}