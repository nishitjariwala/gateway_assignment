using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Source_Control_Final_Assignment.Models;
using System.Web.Security;

namespace Source_Control_Final_Assignment.Controllers
{
    public class AuthController : Controller
    {
        SourceControlEntities db = new SourceControlEntities();

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(AuthController));
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Employee emp)
        {
            try
            {
                var auth = db.Employees.Where(a => a.Username.Equals(emp.Username) && a.Password.Equals(emp.Password)).FirstOrDefault();
                if (auth != null)
                {
                    FormsAuthentication.SetAuthCookie(emp.Username, false);
                    log.Info("Login SuccessFul");
                    return RedirectToAction("Index", "Employee");
                }
            }
            catch(Exception e)
            {
                log.Error(e.ToString());
            }
            
            ModelState.AddModelError("","Your Login Credentials are Wrong");
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Employee emp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(emp.Username, false);
                    log.Info("Signup SuccessFul");
                    return RedirectToAction("Index", "Employee");
                }
                catch(Exception e)
                {
                    log.Error(e.ToString());
                    ModelState.AddModelError("", "Exception is "+e.ToString());
                }

            }
            
            return View();
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Employee");
        }
    }
}