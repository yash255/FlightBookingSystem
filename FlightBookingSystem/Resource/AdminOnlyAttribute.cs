using FlightBookingSystem;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;

public class AdminOnlyAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        var user = filterContext.HttpContext.User;

        // Check if the user is authenticated and has the Admin role
        if (!user.Identity.IsAuthenticated || !(user.Identity is ClaimsIdentity claimsIdentity) || !claimsIdentity.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == UserRole.Admin))
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "Unauthorized" }));
        }
    }

}
