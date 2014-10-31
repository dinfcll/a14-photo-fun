using System.Web.Mvc;
using System.IO;
using PhotoFun.Models;
namespace PhotoFun.Controllers
{
    public class ImporterController : Controller
    {
       
        //
        // GET: /Importer/

        public ActionResult TransfertReussi()
        {
            return View();
        }
        public ActionResult TransfertEchoue()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(PhotoModels model)
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
                    }
                    else
                    {
                        return RedirectToAction("TransfertEchoue");
                    }
                }
                else
                {
                    return RedirectToAction("TransfertEchoue");
                }
            }
            else
            {
                return RedirectToAction("TransfertEchoue");
            }

            return RedirectToAction("TransfertReussi");
        }
    }
}
