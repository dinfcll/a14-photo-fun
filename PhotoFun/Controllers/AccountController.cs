using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using PhotoFun.Filters;
using PhotoFun.Models;
using WebMatrix.WebData;

namespace PhotoFun.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("", "Le nom d'utilisateur ou mot de passe fourni est incorrect.");
            return View(model);
        }

        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            var requeteutilBd = new RequeteUtilBD();
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    if (requeteutilBd.InsererUtil(model))
                    {
                        WebSecurity.Login(model.UserName, model.Password);
                        return RedirectToAction("Index", "Home");
                    } 
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult PhotoUtil(string nom)
        {
            var requetephotoBd = new RequetePhotoBD();
            List<string> lstimage;
            if (requetephotoBd.ExtrairePhotoSelonUtil(nom, out lstimage))
            {
                ViewData["lstimage"]=lstimage;
                ViewData["nom"] = nom;
                if (nom != User.Identity.Name)
                {
                    ViewBag.Title = "Les photos de " + nom;
                }
                else
                {
                    ViewBag.Title = "Mes Photos";
                }
                return View();
            }
            return RedirectToAction("Erreur", "Home");
        }

        [HttpPost]
        public ActionResult PhotoUtil(string image, string actionAFaire,string nom)
        {
            var requeteRelUtilPhotoBd = new RequeteRelUtilPhotoBD();
            var requetephotoBd = new RequetePhotoBD();
            if (image != null && actionAFaire == "LIKE")
            {
                if (requeteRelUtilPhotoBd.VerifLiaisonPhotoUtil(User.Identity.Name, image))
                {
                    if (requeteRelUtilPhotoBd.AjoutRelationUtilPhoto(User.Identity.Name, image))
                    {
                        if (requetephotoBd.AjouterUnLike(image))
                        {
                            return RedirectToAction("PhotoUtil", "Account", new {nom });
                        }
                    }
                }
                else
                {
                    if (requeteRelUtilPhotoBd.EnleveLiaisonPhotoUtil(User.Identity.Name, image))
                    {
                        if (requetephotoBd.EnleveUnLike(image))
                        {
                            return RedirectToAction("PhotoUtil", "Account", new {nom });
                        }
                    }
                }
            }
            else
            {
                if (image != null && actionAFaire == "Supprimer")
                {
                    if (requeteRelUtilPhotoBd.EnleveTousLesLiaisonsAvecLesUtils(image))
                    {
                        if (requetephotoBd.DetruirePhotoSelonUtil(User.Identity.Name, image))
                        {
                            return RedirectToAction("PhotoUtil", "Account", new {nom });
                        }
                    }
                }
                else
                {
                    if (image != null && actionAFaire == "EDIT")
                    {
                        return RedirectToAction("EditCommentaireUtil", "Account", new {image });
                    }
                }
            }
            return RedirectToAction("Erreur", "Home");
        }

        public ActionResult EditCommentaireUtil(string image)
        {
            var requetephotoBd = new RequetePhotoBD();
            var commentaire = requetephotoBd.ExtraireCommentaireSelonPhoto(image);
            ViewData["commentaire"] = commentaire;
            ViewData["image"] = image;
            return View();
        }

        [HttpPost]
        public ActionResult EditCommentaireUtil(string image, string commentaire)
        {
            var requetephotoBd = new RequetePhotoBD();
            string nouveaucommentaire = commentaire; 
            string images= image;
            if (requetephotoBd.MettreAJourLeCommentaireDeLaPhoto(nouveaucommentaire, images))
            {
                return RedirectToAction("PhotoUtil", "Account", new { nom = User.Identity.Name });
            }
            return RedirectToAction("Erreur", "Home");
        }

        public ActionResult Profil(string viewdata)
        {
            var requeteutilBd = new RequeteUtilBD();
            var requeteAbonnementUtilBd = new RequeteAbonnementUtilBD();
            var profilModel = new ProfilModel();
            string courriel;
            string nom;
            string prenom;
            int nbAbonnement;

            profilModel.IdUtilRechercher = User.Identity.Name;

            if (requeteutilBd.ExtraireCourrielSelonUtil(User.Identity.Name, out courriel))
            {
                profilModel.Courriel = courriel;
            }
            if (requeteutilBd.ExtraireNomSelonUtil(User.Identity.Name, out nom))
            {
                profilModel.NomUtil = nom;
            }
            if (requeteutilBd.ExtrairePrenomSelonUtil(User.Identity.Name, out prenom))
            {
                profilModel.PrenomUtil = prenom;
            }
            if (requeteAbonnementUtilBd.CompteNbAbonnement(profilModel, out nbAbonnement))
            {
                profilModel.NbAbonnement = nbAbonnement;
            }
            switch (viewdata)
            {
                case "TransfertEchoue": ViewData["VerifierImporter"] = "TransfertEchoue";
                    break;
                case "MauvaisFichier": ViewData["VerifierImporter"] = "MauvaisFichier";
                    break;
            }
            ViewData["Utilisateur"] = profilModel;

            return View();
        }

        [HttpPost]
        public ActionResult Profil()
        {
            PhotoModels photoModels = new PhotoModels();
            var requetephotoBd = new RequetePhotoBD();
            var requeteUtilBd = new RequeteUtilBD();
            string path = Server.MapPath("~/Images/");
            photoModels.Util = User.Identity.Name;
            photoModels.Categorie = "PhotoProfil";

            if (Request.Files.Count > 0)
            {
                var fichier = Request.Files[0];

                if (fichier != null && fichier.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fichier.FileName);

                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".JPG" || ext == ".PNG" || ext == ".JPEG")
                    {
                        string nomfich = photoModels.Util + '_' + Path.GetFileNameWithoutExtension(fichier.FileName) + photoModels.IdUniqueNomPhoto + ext;
                        string name = "/Images/" + nomfich;
                        const int hauteur = 600;
                        const int largeur = 600;
                        try
                        {
                            var image = Image.FromStream(fichier.InputStream, true, true);
                            if (image.Height >= hauteur && image.Width >= largeur)
                            {
                                fichier.SaveAs(path + nomfich);
                                photoModels.Image = name;
                                requetephotoBd.EnregistrerPhoto(photoModels);
                                requeteUtilBd.MettreAJourPhotoProfil(photoModels.Image, photoModels.Util);
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
                        catch (Exception)
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
            return RedirectToAction("Profil", "Account", new { viewdata = ViewData["VerifierImporter"] });
        }

        public ActionResult MesAbonnements()
        {
            var requeteAbonnementUtilBd = new RequeteAbonnementUtilBD();
            List<string> mesAbonnements;

            if (requeteAbonnementUtilBd.ExtraireLesAbonnementsSelonUtil(User.Identity.Name, out mesAbonnements))
            {
                ViewData["MesAbonnements"] = mesAbonnements;
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RechercheUtil()
        {
            if (Request.TotalBytes > 0)
            {
                var profilModel = new ProfilModel();
                var profilModeltab = new List<ProfilModel>();
                var requeteutilBd = new RequeteUtilBD();
                var requeteAbonnementUtilBd = new RequeteAbonnementUtilBD();
                var strings = Request.Form.GetValues(0);
                if (strings != null)
                {
                    var nomUtil = strings.GetValue(0);

                    List<string> retour;
                    if (requeteutilBd.ExtraireUtilAvecPourcent(nomUtil.ToString(), out retour))
                    {
                        if (retour.Count > 0 && nomUtil.ToString() != "")
                        {
                            foreach (string util in retour)
                            {
                                profilModel = new ProfilModel {IdUtilRechercher = util};
                                int nbAbonnement;
                                if (requeteAbonnementUtilBd.CompteNbAbonnement(profilModel, out nbAbonnement))
                                {
                                    profilModel.NbAbonnement = nbAbonnement;
                                }
                                profilModel.Abonner = requeteAbonnementUtilBd.VerifAbonnement(profilModel.IdUtilRechercher, User.Identity.Name);
                                profilModeltab.Add(profilModel);
                            }
                            ViewData["Rechercher"] = profilModeltab;
                        }
                        else
                        {
                            profilModel.Abonner = false;
                            profilModel.IdUtilRechercher = "Utilisateur Inexistant";
                            profilModel.NbAbonnement = 0;
                            profilModeltab.Add(profilModel);
                            ViewData["Rechercher"] = profilModeltab;
                        }
                    }
                    else
                    {
                        return RedirectToAction("Erreur", "Home");
                    }
                }
            }
            else
            {
                const string erreur = "Une erreur est survenue";
                ViewData["Rechercher"] = erreur;
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult ProfilUtil(string nomUtil)
        {
            var requeteutilBd = new RequeteUtilBD();
            var requeteAbonnementUtilBd = new RequeteAbonnementUtilBD();
            var profilModel = new ProfilModel();
            List<string> retour;

            if(User.Identity.Name!=null)
            {
                if (nomUtil == User.Identity.Name)
                {
                    return RedirectToAction("Profil", "Account");
                }
            }
            if (requeteutilBd.ExtraireUtil(nomUtil, out retour))
                if (retour.Count > 0)
                {
                    profilModel.IdUtilRechercher = nomUtil;

                    int nbAbonnement;
                    if (requeteAbonnementUtilBd.CompteNbAbonnement(profilModel, out nbAbonnement))
                    {
                        profilModel.NbAbonnement = nbAbonnement;
                    }
                    profilModel.Abonner = requeteAbonnementUtilBd.VerifAbonnement(profilModel.IdUtilRechercher,
                        User.Identity.Name);
                    string courriel;
                    if (requeteutilBd.ExtraireCourrielSelonUtil(nomUtil, out courriel))
                    {
                        profilModel.Courriel = courriel;
                    }
                    string nom;
                    if (requeteutilBd.ExtraireNomSelonUtil(nomUtil, out nom))
                    {
                        profilModel.NomUtil = nom;
                    }
                    string prenom;
                    if (requeteutilBd.ExtrairePrenomSelonUtil(nomUtil, out prenom))
                    {
                        profilModel.PrenomUtil = prenom;
                    }
                    ViewData["Rechercher"] = profilModel;
                }
            return View();
        }

        public ActionResult Suivre(string nom, bool abonne)
        {
            var requeteAbonnementUtilBd = new RequeteAbonnementUtilBD();
            var profilModel = new ProfilModel();
            
            if (abonne)
            {
                requeteAbonnementUtilBd.SupprimerRelAbonnement(nom, User.Identity.Name);
            }
            else
            {
                requeteAbonnementUtilBd.AbonnerUtil(User.Identity.Name, nom);
                profilModel.Abonner = true;
            }

            return RedirectToAction("ProfilUtil", "Account", new { nomUtil=nom });
        }

        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Votre mot de passe a été modifié."
                : message == ManageMessageId.SetPasswordSuccess ? "Votre mot de passe a été défini."
                : message == ManageMessageId.RemoveLoginSuccess ? "La connexion externe a été supprimée."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    ModelState.AddModelError("", "Le mot de passe actuel est incorrect ou le nouveau mot de passe n'est pas valide.");
                }
            }
            else
            {
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }
            return View(model);
        }

        #region Applications auxiliaires
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Le nom d'utilisateur existe déjà. Entrez un nom d'utilisateur différent.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Un nom d'utilisateur pour cette adresse de messagerie existe déjà. Entrez une adresse de messagerie différente.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Le mot de passe fourni n'est pas valide. Entrez une valeur de mot de passe valide.";

                case MembershipCreateStatus.InvalidEmail:
                    return "L'adresse de messagerie fournie n'est pas valide. Vérifiez la valeur et réessayez.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "La réponse de récupération du mot de passe fournie n'est pas valide. Vérifiez la valeur et réessayez.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "La question de récupération du mot de passe fournie n'est pas valide. Vérifiez la valeur et réessayez.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Le nom d'utilisateur fourni n'est pas valide. Vérifiez la valeur et réessayez.";

                case MembershipCreateStatus.ProviderError:
                    return "Le fournisseur d'authentification a retourné une erreur. Vérifiez votre entrée et réessayez. Si le problème persiste, contactez votre administrateur système.";

                case MembershipCreateStatus.UserRejected:
                    return "La demande de création d'utilisateur a été annulée. Vérifiez votre entrée et réessayez. Si le problème persiste, contactez votre administrateur système.";

                default:
                    return "Une erreur inconnue s'est produite. Vérifiez votre entrée et réessayez. Si le problème persiste, contactez votre administrateur système.";
            }
        }
        #endregion
    }
}
