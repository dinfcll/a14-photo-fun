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
            string path= Server.MapPath("Images/");

            if (Request.Files.Count > 0)
            {
                var fichier= Request.Files[0];
                
                if(fichier != null && fichier.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fichier.FileName);
                    
                    if(ext == ".jpg" || ext==".png")
                    {
                        fichier.SaveAs(path + fichier.FileName);
                        string name= "~/Images/"+fichier.FileName;
                        model.image = name;
                    }
                }
            }           
            
            return RedirectToAction("UploadDocument");
        }
    }
}
