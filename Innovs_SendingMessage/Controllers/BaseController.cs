using Innovs.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Innovs_SendingMessage.Controllers
{
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session["user"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Users/Login");
        }
    }
    public class BaseController : Controller
    {
        public static Innovs.Service.User.User UserService;
        public static Innovs.Service.Mobile.MobileInfo MobileInfoService;
        public static Innovs.Service.Mobile.MessageLog MessageLogService;
        public BaseController()
        {
            UserService = new Innovs.Service.User.User();
            MobileInfoService = new Innovs.Service.Mobile.MobileInfo();
            MessageLogService = new Innovs.Service.Mobile.MessageLog();
        }
        public User CurrentUser()
        {
           return ((User)Session["user"]);
        }
    }
}