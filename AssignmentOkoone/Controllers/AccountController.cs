using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentOkoone.Models;
using OkooneAssessment.Models;

namespace AssignmentOkoone.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private VASEntities db = new VASEntities();
        Helper AES = new Helper();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Session.Remove("userProfile");
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountModel model)
        {
            var password = AES.Encrypt(model.password);
            var result = db.USERs.FirstOrDefault(r => r.USER_NAME == model.user_name && r.PASSWORD == password);
            if (result == null)
            {
                return View();
            }
            else
            {
                Session["userProfile"] = "Success";
                return Redirect("/Home/Index");
            }

        }
    }
}