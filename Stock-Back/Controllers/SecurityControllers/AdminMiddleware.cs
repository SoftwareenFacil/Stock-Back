using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class SuperAdminRequiredAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (user == null || user.Identity == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        var userClaim = user.Claims.FirstOrDefault(c => c.Type == "name");

        var superAdminClaim = user.Claims.FirstOrDefault(c => c.Type == "SuperAdmin");
        if (superAdminClaim == null || superAdminClaim.Value != "True")
        {
            context.Result = new ForbidResult();
        }
    }
}
