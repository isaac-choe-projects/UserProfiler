using EPShared.Shared_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace EntertainmentProfiler.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string isAuthenticationCode = "")
        {
            ViewBag.isAuthenticationCode = isAuthenticationCode;
            return View();
        }

        public ActionResult AuthenticateUser(string passedInUserName, string passedInPassword)
        {
            CustomerInfo customerInfoObj = new CustomerInfo();
            var isAuthenticated = customerInfoObj.AuthenticateUserLogin(passedInUserName, passedInPassword);

            // If authenticated then return to home page
            if (isAuthenticated == true)
            {
                this.Session["UserProfile"] = customerInfoObj.FindUserByUserName(passedInUserName);
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Home", action = "Index", passedInIsAuthenticated = isAuthenticated }));
            }
            // Otherwise return to the login page and notify the login was unsuccessful
            else
            {
                return RedirectToAction("Index", new { isAuthenticationCode = "AUTHENTICATION-FAILED" });
            }
        }

        public ActionResult SignOut()
        {
            this.Session["UserProfile"] = null;
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Home", action = "Index" }));
        }
    }
}