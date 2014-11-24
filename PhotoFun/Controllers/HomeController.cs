﻿using System.Collections.Generic;
using System.Web.Mvc;
using PhotoFun.Models;
using System.IO;

namespace PhotoFun.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var requetephotoBD = new RequetePhotoBD();
            List<string> lstimage;
            if (requetephotoBD.ExtraireDernieresPhotos(5, out lstimage))
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

        [HttpGet]
        public ActionResult Importer(string viewdata)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (viewdata == "TransfertReussi")
                {
                    ViewData["VerifierImporter"] = "TransfertReussi";
                }
                else
                {
                    if (viewdata == "TransfertEchoue")
                    {
                        ViewData["VerifierImporter"] = "TransfertEchoue";
                    }
                }
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Contact()
        {
            return View();
        }
       
        public ActionResult RetourneLaVueSelonCategorie(string categorie)
        {
            var requetephotoBD = new RequetePhotoBD();
            List<string> lstimage;
            if (requetephotoBD.ExtrairePhotoSelonCategorie(categorie, out lstimage))
            {
                ViewData["lstimage"] = lstimage;
                ViewBag.Title = categorie;
                return View();
            }
            return RedirectToAction("Erreur", "Home");
        }

        [HttpPost]
        public ActionResult RetourneLaVueSelonCategorie(string categorie,string image)
        {
            if (image != null && categorie != null)
            {
                var requeteRelUtilPhotoBD = new RequeteRelUtilPhotoBD();
                var requetephotoBD = new RequetePhotoBD();
                if (requeteRelUtilPhotoBD.VerifLiaisonPhotoUtil(User.Identity.Name, image))
                {
                    if (requeteRelUtilPhotoBD.AjoutRelationUtilPhoto(User.Identity.Name, image))
                    {
                        if (requetephotoBD.AjouterUnLike(image))
                        {
                            return RedirectToAction("RetourneLaVueSelonCategorie", "Home", new { categorie = categorie });
                        }
                    }
                }
                else
                {
                    if (requeteRelUtilPhotoBD.EnleveLiaisonPhotoUtil(User.Identity.Name, image))
                    {
                        if (requetephotoBD.EnleveUnLike(image))
                        {
                            return RedirectToAction("RetourneLaVueSelonCategorie", "Home", new { categorie = categorie });
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Erreur", "Home");
        }

        [HttpPost]
        public ActionResult Importer(PhotoModels model)
        {
            model.util = User.Identity.Name;
            var requetephotoBD = new RequetePhotoBD();
            string path= Server.MapPath("~/Images/");
            string NouveauNomPhoto = model.util + "_";

            if (Request.Files.Count > 0)
            {
                var fichier = Request.Files[0];

                if (fichier != null && fichier.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fichier.FileName);

                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                    {
                        string nomfich = model.util+ '_' + Path.GetFileNameWithoutExtension(fichier.FileName) + model.IDUniqueNomPhoto + ext;
                        string name = "/Images/" +nomfich;
                        fichier.SaveAs(path + nomfich);
                        model.image = name;

                        requetephotoBD.EnregistrerPhoto(model);
                        ViewData["VerifierImporter"] = "TransfertReussi";
                    }
                    else
                    {
                         ViewData["VerifierImporter"] = "TransfertEchoue";
                    }
                }
                else
                {
                    ViewData["VerifierImporter"] = "TransfertEchoue";
                }
            }
            else
            {
                ViewData["VerifierImporter"] = "TransfertEchoue";
            }

            return RedirectToAction("Importer", "Home", new { viewdata = ViewData["VerifierImporter"] });
        }
    }
}
