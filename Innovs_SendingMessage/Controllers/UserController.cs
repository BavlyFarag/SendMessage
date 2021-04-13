using Innovs.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Innovs_SendingMessage.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            ViewBag.Error = string.Empty;

            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            var UserResult = UserService.SearchFor(c => c.UserName == user.UserName && c.Password == user.Password).FirstOrDefault();
            if (UserResult != null)
            {
                if (UserResult.IsActive == true)
                {
                    Session["user"] = UserResult;

                    return RedirectToAction("ViewMobilesInfo", "Mobile");
                }
                else
                {
                    ViewBag.Error = "User name Deactive";
                    return View();
                }
            }
            else
            {
                ViewBag.Error = "User name or password is incorrect";
                return View();
            }
        }

    }
}