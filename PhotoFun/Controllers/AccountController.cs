using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using PhotoFun.Filters;
using PhotoFun.Models;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

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
            var requeteutilBD = new RequeteUtilBD();
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    if (requeteutilBD.InsererUtil(model))
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
            var requetephotoBD = new RequetePhotoBD();
            List<string> lstimage;
            if (requetephotoBD.ExtrairePhotoSelonUtil(nom, out lstimage))
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
            var requeteRelUtilPhotoBD = new RequeteRelUtilPhotoBD();
            var requetephotoBD = new RequetePhotoBD();
            if (image != null && actionAFaire == "LIKE")
            {
                if (requeteRelUtilPhotoBD.VerifLiaisonPhotoUtil(User.Identity.Name, image))
                {
                    if (requeteRelUtilPhotoBD.AjoutRelationUtilPhoto(User.Identity.Name, image))
                    {
                        if (requetephotoBD.AjouterUnLike(image))
                        {
                            return RedirectToAction("PhotoUtil", "Account", new { nom = nom });
                        }
                    }
                }
                else
                {
                    if (requeteRelUtilPhotoBD.EnleveLiaisonPhotoUtil(User.Identity.Name, image))
                    {
                        if (requetephotoBD.EnleveUnLike(image))
                        {
                            return RedirectToAction("PhotoUtil", "Account", new { nom = nom });
                        }
                    }
                }
            }
            else
            {
                if (image != null && actionAFaire == "Supprimer")
                {
                    if (requeteRelUtilPhotoBD.EnleveTousLesLiaisonsAvecLesUtils(image))
                    {
                        if (requetephotoBD.DetruirePhotoSelonUtil(User.Identity.Name, image))
                        {
                            return RedirectToAction("PhotoUtil", "Account", new { nom = nom });
                        }
                    }
                }
                else
                {
                    if (image != null && actionAFaire == "EDIT")
                    {
                        return RedirectToAction("EditCommentaireUtil", "Account", new { image = image });
                    }
                }
            }
            return RedirectToAction("Erreur", "Home");
        }

        public ActionResult EditCommentaireUtil(string image)
        {
            string commentaire;
            var requetephotoBD = new RequetePhotoBD();
            commentaire = requetephotoBD.ExtraireCommentaireSelonPhoto(image);
            ViewData["commentaire"] = commentaire;
            ViewData["image"] = image;
            return View();
        }

        [HttpPost]
        public ActionResult EditCommentaireUtil(string Image, string Commentaire)
        {
            var requetephotoBD = new RequetePhotoBD();
            string nouveaucommentaire = Commentaire; 
            string image= Image;
            if (requetephotoBD.MettreAJourLeCommentaireDeLaPhoto(nouveaucommentaire, image))
            {
                return RedirectToAction("PhotoUtil", "Account", new { nom = User.Identity.Name });
            }
            return RedirectToAction("Erreur", "Home");
        }

        public ActionResult Profil(string viewdata)
        {
            var requeteutilBD = new RequeteUtilBD();
            var requeteAbonnementUtilBD = new RequeteAbonnementUtilBD();
            var profilModel = new ProfilModel();
            var courriel = "";
            var nom = "";
            var prenom = "";
            int nbAbonnement;

            profilModel.IdUtilRechercher = User.Identity.Name;

            if (requeteutilBD.ExtraireCourrielSelonUtil(User.Identity.Name, out courriel))
            {
                profilModel.Courriel = courriel;
            }
            if (requeteutilBD.ExtraireNomSelonUtil(User.Identity.Name, out nom))
            {
                profilModel.NomUtil = nom;
            }
            if (requeteutilBD.ExtrairePrenomSelonUtil(User.Identity.Name, out prenom))
            {
                profilModel.PrenomUtil = prenom;
            }
            if (requeteAbonnementUtilBD.CompteNbAbonnement(profilModel, out nbAbonnement))
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
            var requetephotoBD = new RequetePhotoBD();
            var requeteUtilBD = new RequeteUtilBD();
            string path = Server.MapPath("~/Images/");
            photoModels.util = User.Identity.Name;
            string NouveauNomPhoto = photoModels.util + "_";
            photoModels.Categorie = "PhotoProfil";

            if (Request.Files.Count > 0)
            {
                var fichier = Request.Files[0];

                if (fichier != null && fichier.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fichier.FileName);

                    if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".JPG" || ext == ".PNG" || ext == ".JPEG")
                    {
                        string nomfich = photoModels.util + '_' + Path.GetFileNameWithoutExtension(fichier.FileName) + photoModels.IDUniqueNomPhoto + ext;
                        string name = "/Images/" + nomfich;
                        Image image;
                        int Hauteur=600, Largeur=600;
                        try
                        {
                            image = Image.FromStream(fichier.InputStream, true, true);
                            if (image.Height >= Hauteur && image.Width >= Largeur)
                            {
                                fichier.SaveAs(path + nomfich);
                                photoModels.image = name;
                                requetephotoBD.EnregistrerPhoto(photoModels);
                                requeteUtilBD.MettreAJourPhotoProfil(photoModels.image, photoModels.util);
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
            var requeteAbonnementUtilBD = new RequeteAbonnementUtilBD();
            List<string> MesAbonnements = new List<string>();

            if (requeteAbonnementUtilBD.ExtraireLesAbonnementsSelonUtil(User.Identity.Name, out MesAbonnements))
            {
                ViewData["MesAbonnements"] = MesAbonnements;
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
                var requeteutilBD = new RequeteUtilBD();
                var requeteAbonnementUtilBD = new RequeteAbonnementUtilBD();
                var retour = new List<string>();
                int nbAbonnement;
                var nomUtil = Request.Form.GetValues(0).GetValue(0);

                if (requeteutilBD.ExtraireUtilAvecPourcent(nomUtil.ToString(), out retour))
                {
                    if (retour.Count > 0 && nomUtil.ToString() != "")
                    {
                        foreach (string util in retour)
                        {
                            profilModel = new ProfilModel();
                            profilModel.IdUtilRechercher = util;
                            if (requeteAbonnementUtilBD.CompteNbAbonnement(profilModel, out nbAbonnement))
                            {
                                profilModel.NbAbonnement = nbAbonnement;
                            }
                            profilModel.Abonner = requeteAbonnementUtilBD.VerifAbonnement(profilModel.IdUtilRechercher, User.Identity.Name);
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
            else
            {
                string erreur = "Une erreur est survenue";
                ViewData["Rechercher"] = erreur;
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult ProfilUtil(string nomUtil)
        {
            var requeteutilBD = new RequeteUtilBD();
            var requeteAbonnementUtilBD = new RequeteAbonnementUtilBD();
            var profilModel = new ProfilModel();
            var retour = new List<string>();
            var courriel="";
            var nom="";
            var prenom="";
            int nbAbonnement;

            if(User.Identity.Name!=null)
            {
                if (nomUtil == User.Identity.Name)
                {
                    return RedirectToAction("Profil", "Account");
                }
            }
            if (requeteutilBD.ExtraireUtil(nomUtil, out retour))
            {
                if (retour.Count > 0)
                {
                    profilModel.IdUtilRechercher = nomUtil;

                    if (requeteAbonnementUtilBD.CompteNbAbonnement(profilModel, out nbAbonnement))
                    {
                        profilModel.NbAbonnement = nbAbonnement;
                    }
                    profilModel.Abonner = requeteAbonnementUtilBD.VerifAbonnement(profilModel.IdUtilRechercher, User.Identity.Name);
                    if (requeteutilBD.ExtraireCourrielSelonUtil(nomUtil,out courriel))
                    {
                        profilModel.Courriel = courriel;
                    }
                    if (requeteutilBD.ExtraireNomSelonUtil(nomUtil, out nom))
                    {
                        profilModel.NomUtil = nom;
                    }
                    if (requeteutilBD.ExtrairePrenomSelonUtil(nomUtil, out prenom))
                    {
                        profilModel.PrenomUtil = prenom;
                    }
                    ViewData["Rechercher"] = profilModel;
                }
            }
            return View();
        }

        public ActionResult Suivre(string nom, bool Abonne)
        {
            var requeteAbonnementUtilBD = new RequeteAbonnementUtilBD();
            var profilModel = new ProfilModel();
            
            if (Abonne)
            {
                requeteAbonnementUtilBD.SupprimerRelAbonnement(nom, User.Identity.Name);
            }
            else
            {
                requeteAbonnementUtilBD.AbonnerUtil(User.Identity.Name, nom);
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
