using System.Collections.Generic;
using System.Web.Mvc;
using System;
using PhotoFun.Models;
using System.IO;
using System.Drawing;

namespace PhotoFun.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var requetephotoBd = new RequetePhotoBd();
            List<string> lstimage;
            if (requetephotoBd.ExtraireDernieresPhotos(5, out lstimage))
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
                switch (viewdata)
                {
                    case "TransfertReussi": ViewData["VerifierImporter"] = "TransfertReussi";
                        break;
                    case "TransfertEchoue": ViewData["VerifierImporter"] = "TransfertEchoue";
                        break;
                    case "MauvaisFichier": ViewData["VerifierImporter"] = "MauvaisFichier";
                        break;
                }
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult PartagerImage(string image)
        {
            string host= Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            RequetePhotoBd requetePhotoBd = new RequetePhotoBd();
            var commentaire = requetePhotoBd.ExtraireCommentaireSelonPhoto(image);
            ViewData["commentaire"] = commentaire;
            ViewData["host"] = host;
            ViewData["image"] = image;
            return View();
        }
		
        public ActionResult RetourneLaVueSelonCategorie(string categorie)
        {
            var requetephotoBd = new RequetePhotoBd();
            List<string> lstimage;
            if (requetephotoBd.ExtrairePhotoSelonCategorie(categorie, out lstimage))
            {
                ViewData["lstimage"] = lstimage;
                ViewBag.Title = categorie;
                return View();
            }
            return RedirectToAction("Erreur", "Home");
        }

        [HttpPost]
        public ActionResult RetourneLaVueSelonCategorie(string categorie, string image)
        {
            if (image != null && categorie != null)
            {
                var requeteRelUtilPhotoBd = new RequeteRelUtilPhotoBd();
                var requetephotoBd = new RequetePhotoBd();
                if (requeteRelUtilPhotoBd.VerifLiaisonPhotoUtil(User.Identity.Name, image))
                {
                    if (requeteRelUtilPhotoBd.AjoutRelationUtilPhoto(User.Identity.Name, image))
                    {
                        if (requetephotoBd.AjouterUnLike(image))
                        {
                            return RedirectToAction("RetourneLaVueSelonCategorie", "Home", new {categorie });
                        }
                    }
                }
                else
                {
                    if (requeteRelUtilPhotoBd.EnleveLiaisonPhotoUtil(User.Identity.Name, image))
                    {
                        if (requetephotoBd.EnleveUnLike(image))
                        {
                            return RedirectToAction("RetourneLaVueSelonCategorie", "Home", new {categorie });
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
            model.Util = User.Identity.Name;
            var requetephotoBd = new RequetePhotoBd();
            string path = Server.MapPath("~/Images/");

            if (Request.Files.Count > 0)
            {
                var fichier = Request.Files[0];

                if (fichier != null && fichier.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fichier.FileName);
                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext==".JPG" || ext==".PNG" || ext==".JPEG")
                    {
                        string nomfich = model.Util+ '_' + Path.GetFileNameWithoutExtension(fichier.FileName) + model.IdUniqueNomPhoto + ext;
                        string name = "/Images/" +nomfich;
                        const int hauteur = 600;
                        const int largeur = 600;
                        try
                        {
                            var image = Image.FromStream(fichier.InputStream, true, true);
                            if (image.Height >= hauteur && image.Width >= largeur)
                            {
                                fichier.SaveAs(path + nomfich);
                                model.Image = name;
                                requetephotoBd.EnregistrerPhoto(model);
                                ViewData["VerifierImporter"] = "TransfertReussi";
                            }
                            else
                            {
                              ViewData["VerifierImporter"] = "TransfertEchoue";
                            }
                        }
                        catch (ArgumentException)
                        {
                            ViewData["VerifierImporter"] = "MauvaisFichier";
                        }
                        catch(Exception)
                        {
                            return RedirectToAction("Erreur", "Home");
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
            }
            else
            {
                ViewData["VerifierImporter"] = "TransfertEchoue";
            }

            return RedirectToAction("Importer", "Home", new { viewdata = ViewData["VerifierImporter"] });
        }
    }
}
