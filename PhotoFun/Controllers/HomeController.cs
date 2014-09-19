using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoFun.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Importer()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
               return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
