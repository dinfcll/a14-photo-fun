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
            ViewBag.Message = "Modifiez ce modèle pour dynamiser votre application ASP.NET MVC.";

            return View();
        }

        public ActionResult Importer()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Message = "Votre page de description d’application.";

                return View();
            }
            else
            {
                return View();//À retourner vers la vue Login
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Votre page de contact.";

            return View();
        }
    }
}
