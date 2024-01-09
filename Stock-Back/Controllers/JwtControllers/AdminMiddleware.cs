using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class SuperAdminRequiredAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (user.Identity != null)
        {
            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var userClaim = user.Claims.FirstOrDefault(c => c.Type == "name");

            var superAdminClaim = user.Claims.FirstOrDefault(c => c.Type == "SuperAdmin");
            if (superAdminClaim == null || superAdminClaim.Value != "True")
            {
                // If the claim is not found or the value is not what is expected
                context.Result = new ForbidResult();
            }
        }
    }
}
