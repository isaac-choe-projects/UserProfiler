﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntertainmentProfiler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool passedInIsAuthenticated = false)
        {
            return View("Index");
        }
    }
}