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

        [HttpGet]
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
       
        public ActionResult RetourneLaVueSelonCategorie(string categorie)
        {
            var pfbd = new PhotoFunBD();
            List<string> lstimage;
            if (pfbd.ExtrairePhotoSelonCategorie(categorie, out lstimage))
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
                var pfbd = new PhotoFunBD();
                if (pfbd.VerifLiaisonPhotoUtil(User.Identity.Name, image))
                {
                    if (pfbd.AjoutRelationUtilPhoto(User.Identity.Name, image))
                    {
                        if (pfbd.AjouterUnLike(image))
                        {
                            return RedirectToAction("RetourneLaVueSelonCategorie", "Home", new { categorie = categorie });
                        }
                    }
                }
                else
                {
                    if(pfbd.EnleveLiaisonPhotoUtil(User.Identity.Name,image))
                    {
                        if (pfbd.EnleveUnLike(image))
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
            var ajouterphoto = new PhotoFunBD();
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

                        ajouterphoto.EnregistrerPhoto(model);
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
            
            return View();
        }
    }
}
