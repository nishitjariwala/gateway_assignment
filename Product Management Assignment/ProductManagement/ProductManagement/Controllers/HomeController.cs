﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //For Display The Username on Home Page
        public ActionResult Index()
        {
            return View();
        }

       
    }
}