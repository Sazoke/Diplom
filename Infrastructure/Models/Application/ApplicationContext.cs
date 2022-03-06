using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models.Application;

public class ApplicationContext
{
    public ApplicationContext(IHttpContextAccessor httpContextAccessor, ApplicationUserManager usermanager)
    {
        if (httpContextAccessor.HttpContext?.User.Identity != null && 
            httpContextAccessor.HttpContext != null && 
            !httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            return;
        var id = usermanager.GetUserId(httpContextAccessor.HttpContext?.User);
        CurrentUser = usermanager.Users.FirstOrDefault(u => u.Id == id);
    }
    public ApplicationUser CurrentUser { get; set; }
}