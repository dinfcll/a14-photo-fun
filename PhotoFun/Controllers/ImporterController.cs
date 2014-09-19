using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Collections;
using System.Threading;
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
        public ActionResult Upload()
        {
            FileStream fs = new FileStream("E:/Implantation d'un système informatique/a14-photo-fun/PhotoFun/Content/FichierText/NomPhoto.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            StreamWriter sw = new StreamWriter(fs);
            string contenuFichier = "";
           
           
            if (Request.Files.Count > 0)
            {
                
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("/Content/Photo"), fileName);
                    file.SaveAs(path);
                    //ajouter le fichier dans un fichier text
                    contenuFichier = fileName+";";
                    Thread.Sleep(10);
                    fs.Seek(0, SeekOrigin.End);
                    sw.Write(contenuFichier);
                              
                }
            }
            sw.Close();
            fs.Close();
            
            
            return RedirectToAction("UploadDocument");
        }
    }
}

