using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentOkoone.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["userProfile"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/Account/Login");
            }
            
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
    }
}