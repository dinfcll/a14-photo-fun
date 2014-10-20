using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Collections;
using System.Threading;
using PhotoFun.Models;
namespace PhotoFun.Controllers
{
    public class ImporterController : Controller
    {
       
        //
        // GET: /Importer/

        public ActionResult UploadDocument()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(PhotoModels model)
        {
            model.Categorie = "autre"; //Temporaire, a ajuster avec radio button
            model.util = "xxdomxx";//idem
            PhotoFunBD Ajouterphoto = new PhotoFunBD();
            string path= Server.MapPath("~/Images/");
            string NouveauNomPhoto = model.util + "_";//NomUtil_NomPhoto

            if (Request.Files.Count > 0)
            {
                var fichier= Request.Files[0];
                
                if(fichier != null && fichier.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fichier.FileName);
                    
                    if(ext == ".jpg" || ext==".png")
                    {
                        NouveauNomPhoto += fichier.FileName;
                        fichier.SaveAs(path + NouveauNomPhoto);
                        string name= "~/Images/"+NouveauNomPhoto;
                        model.image = name;
                        
                        Ajouterphoto.EnregistrerPhoto(model);
                    }
                }
            }           
            
            return RedirectToAction("UploadDocument");
        }
    }
}
