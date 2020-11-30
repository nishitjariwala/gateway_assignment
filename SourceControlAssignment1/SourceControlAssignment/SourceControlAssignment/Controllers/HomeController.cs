using SourceControlAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SourceControlAssignment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult EmployeeDetails(EmployeeClass Emp)
        {
            if (ModelState.IsValid)
            {
                ViewBag.name = "Name: "+Emp.Name;
                ViewBag.email = "Email: "+Emp.Email;
                ViewBag.age = "Age: "+Emp.Age;
                ViewBag.number = "Number: "+Emp.Number;
                ViewBag.status = "Employee Has been Registred Successfully";
                ViewBag.details = "Employee Details:- ";
                


                return View("Index");
            }
            else
            {
                ViewBag.status = "";
                ViewBag.name = "";
                ViewBag.email = "";
                ViewBag.age = "";
                ViewBag.number = "";
                ViewBag.details = "";
                return View("Index");
            }
        }
    }
}