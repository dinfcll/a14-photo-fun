using System.Collections.Generic;
using System.Web.Mvc;
using PhotoFun.Models;

namespace PhotoFun.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var pfbd = new PhotoFunBD();
            List<string> lstimage;
            if (pfbd.ExtraireDernieresPhotos(5, out lstimage))
            {
                ViewData["lstimage"] = lstimage;
                return View();
            }
            return RedirectToAction("Erreur", "Home");
        }
        public ActionResult Erreur()
        {
            return View();
        }

        public ActionResult Importer()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Sport()
        {
            return RetourneLaVueSelonCategorie("Sport");
        }
        
        public ActionResult Nature()
        {
            return RetourneLaVueSelonCategorie("Nature");
        }

        public ActionResult Famille()
        {
            return RetourneLaVueSelonCategorie("Famille");
        }

        public ActionResult Paysage()
        {
            return RetourneLaVueSelonCategorie("Paysage");
        }

        public ActionResult Cuisine()
        {
            return RetourneLaVueSelonCategorie("Cuisine");
        }

        public ActionResult Animaux()
        {
            return RetourneLaVueSelonCategorie("Animaux");
        }

        public ActionResult Autre()
        {
            return RetourneLaVueSelonCategorie("Autres");
        }

        private ActionResult RetourneLaVueSelonCategorie(string categorie)
        {
            var pfbd = new PhotoFunBD();
            List<string> lstimage;
            if (pfbd.ExtrairePhotoSelonCategorie(categorie, out lstimage))
            {
                ViewData["lstimage"] = lstimage;
                return View();
            }
            return RedirectToAction("Erreur", "Home");
        }
    }
}
