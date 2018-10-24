using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolWeb.Custom
{
    public class SecurityFilter : ActionFilterAttribute
    {
        private readonly int _Role;
        public SecurityFilter(int role)
        {
            _Role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;

            if ((session["RoleID"] == null || ((session["RoleID"] != null) && (int)session["RoleID"] > (_Role))))
            {
                filterContext.Result = new RedirectResult("/User/Login", false);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}