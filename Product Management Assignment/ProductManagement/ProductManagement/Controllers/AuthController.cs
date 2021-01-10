using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductManagement.Models;
using System.Web.Security;
namespace ProductManagement.Controllers
{
    // For Authentication
    public class AuthController : Controller
    {
        GatewayEntities db = new GatewayEntities();

        //For Login Get Method
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }


        // For Login POST Method
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                //Checking if User is Authenticated or Not

                var auth = db.Users.Where(a => a.Email.Equals(user.Email) && a.Password.Equals(user.Password)).FirstOrDefault();
                if (!(auth == null))
                {
                    //Setting Authentication Cookies and Session

                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    Session["User_Id"] = auth.User_Id;
                    Session["Username"] = auth.Username;
                    return RedirectToAction("Index", "Home");

                }
            }
            ModelState.AddModelError("", "You Entered Wrong Credentials");
            return View();
        }

        //For Registration Of User
        public ActionResult Signup()
        {
            return View();
        }

        //POST Method For Registration
        [HttpPost]
        public ActionResult Signup(User user)
        {
            if (ModelState.IsValid)
            {
                //Adding User To the Database
                var auth = db.Users.Add(user);
                db.SaveChanges();

                //Setting Authentication Cookies and Session
                FormsAuthentication.SetAuthCookie(user.Username, false);
                Session["User_Id"] = auth.User_Id;
                Session["Username"] = auth.Username;

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "You Entered Wrong Credentials");
            return View();
        }

        // For Signout Of User
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}