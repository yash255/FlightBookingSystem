using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlightBookingSystem.Resource
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        private readonly Resource.AppDbContext _context;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            allowedroles = roles;
            _context = new Resource.AppDbContext();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var user = _context.Users.FirstOrDefault(u => u.Email == httpContext.User.Identity.Name);

            if (user != null && allowedroles.Contains(user.Role))
            {
                authorize = true;
            }

            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }


}