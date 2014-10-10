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
            return RedirectToAction("UploadDocument");
        }
    }
}


