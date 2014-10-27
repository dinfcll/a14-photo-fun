using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoFun.Models;

namespace PhotoFun.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PhotoFunBD pfbd = new PhotoFunBD();
            List<string> lstimage = new List<string>();
            if (pfbd.ExtraireDernieresPhotos(5, out lstimage))
            {
                ViewData["lstimage"] = lstimage;
                return View();
            }
            else
            {
                return RedirectToAction("Erreur", "Home");
            }
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
            PhotoFunBD pfbd = new PhotoFunBD();
            List<string> lstimage = new List<string>();
            if (pfbd.ExtrairePhotoSelonCategorie("Sport", out lstimage))
            {
                ViewData["lstimage"] = lstimage;
                return View();
            }
            else
            {
                return RedirectToAction("Erreur", "Home");
            }
        }
        
        public ActionResult Nature()
        {
            PhotoFunBD pfbd = new PhotoFunBD();
            List<string> lstimage = new List<string>();
            if (pfbd.ExtrairePhotoSelonCategorie("Nature", out lstimage))
            {
                ViewData["lstimage"] = lstimage;
                return View();
            }
            else
            {
                return RedirectToAction("Erreur", "Home");
            }
        }

        public ActionResult Famille()
        {
            PhotoFunBD pfbd = new PhotoFunBD();
            List<string> lstimage = new List<string>();
            if (pfbd.ExtrairePhotoSelonCategorie("Famille", out lstimage))
            {
                ViewData["lstimage"] = lstimage;
                return View();
            }
            else
            {
                return RedirectToAction("Erreur", "Home");
            }
        }

        public ActionResult Paysage()
        {
            PhotoFunBD pfbd = new PhotoFunBD();
            List<string> lstimage = new List<string>();
            if (pfbd.ExtrairePhotoSelonCategorie("Paysage", out lstimage))
            {
                ViewData["lstimage"] = lstimage;
                return View();
            }
            else
            {
                return RedirectToAction("Erreur", "Home");
            }
        }

        public ActionResult Cuisine()
        {
            PhotoFunBD pfbd = new PhotoFunBD();
            List<string> lstimage = new List<string>();
            if (pfbd.ExtrairePhotoSelonCategorie("Cuisine", out lstimage))
            {
                ViewData["lstimage"] = lstimage;
                return View();
            }
            else
            {
                return RedirectToAction("Erreur", "Home");
            }
        }

        public ActionResult Animaux()
        {
            PhotoFunBD pfbd = new PhotoFunBD();
            List<string> lstimage = new List<string>();
            if (pfbd.ExtrairePhotoSelonCategorie("Animaux", out lstimage))
            {
                ViewData["lstimage"] = lstimage;
                return View();
            }
            else
            {
                return RedirectToAction("Erreur", "Home");
            }
        }

        public ActionResult Autre()
        {
            PhotoFunBD pfbd = new PhotoFunBD();
            List<string> lstimage = new List<string>();
            if (pfbd.ExtrairePhotoSelonCategorie("Autres", out lstimage))
            {
                ViewData["lstimage"] = lstimage;
                return View();
            }
            else
            {
                return RedirectToAction("Erreur", "Home");
            }
        }
    }
}
