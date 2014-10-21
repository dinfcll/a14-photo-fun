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

        public ActionResult Sport()
        {
            return View();
        }
        
        public ActionResult Nature()
        {
            return View();
        }

        public ActionResult Famille()
        {
            return View();
        }

        public ActionResult Paysage()
        {
            return View();
        }

        public ActionResult Cuisine()
        {
            return View();
        }

        public ActionResult Animaux()
        {
            return View();
        }

        public ActionResult Autre()
        {
            return View();
        }
    }
}
