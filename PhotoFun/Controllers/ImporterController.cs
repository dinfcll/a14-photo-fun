using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
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
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("/App_Data/"), fileName);
                    file.SaveAs(path);
                }
            }

            return RedirectToAction("UploadDocument");
        }
    }
}

