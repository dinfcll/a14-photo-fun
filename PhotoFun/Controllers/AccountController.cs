using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using PhotoFun.Filters;
using PhotoFun.Models;
using System.Data.SqlClient;

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
            var ajouterUtil = new PhotoFunBD();
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    if (ajouterUtil.InsererUtil(model))
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
            var photoFunBD = new PhotoFunBD();
            List<string> lstimage; 
            if (photoFunBD.ExtrairePhotoSelonUtil(nom, out lstimage))
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
            var pfbd = new PhotoFunBD();
            if (image != null && actionAFaire == "LIKE")
            {
                if (pfbd.VerifLiaisonPhotoUtil(User.Identity.Name, image))
                {
                    if (pfbd.AjoutRelationUtilPhoto(User.Identity.Name, image))
                    {
                        if (pfbd.AjouterUnLike(image))
                        {
                            return RedirectToAction("PhotoUtil", "Account", new { nom = nom });
                        }
                    }
                }
                else
                {
                    if (pfbd.EnleveLiaisonPhotoUtil(User.Identity.Name, image))
                    {
                        if (pfbd.EnleveUnLike(image))
                        {
                            return RedirectToAction("PhotoUtil", "Account", new { nom = nom });
                        }
                    }
                }
            }
            else
            {
                if (image != null && actionAFaire == "DELETE")
                {
                    if (pfbd.EnleveTousLesLiaisonsAvecLesUtils(image))
                    {
                        if (pfbd.DetruirePhotoSelonUtil(User.Identity.Name, image))
                        {
                            return RedirectToAction("PhotoUtil", "Account", new { nom = nom });
                        }
                    }
                }
            }
            return RedirectToAction("Erreur", "Home");
        }

        public ActionResult Profil()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ProfilUtil()
        {
            if (Request.TotalBytes > 0)
            {
                var profilModel = new ProfilModel();
                var photoFunBD = new PhotoFunBD();
                var retour = new List<string>();
                int nbAbonnement;
                var nomUtil = Request.Form.GetValues(0).GetValue(0);
                if (photoFunBD.ExtraireUtil(nomUtil.ToString(), out retour))
                {
                    if (retour.Count > 0)
                    {
                        profilModel.IdUtilRechercher = nomUtil.ToString();

                        if (photoFunBD.CompteNbAbonnement(profilModel, out nbAbonnement))
                        {
                            profilModel.NbAbonnement = nbAbonnement;
                        }
                        profilModel.Abonner = photoFunBD.VerifAbonnement(profilModel.IdUtilRechercher, User.Identity.Name);
                        ViewData["Rechercher"] = profilModel;
                    }
                    else
                    {
                        profilModel.Abonner = false;
                        profilModel.IdUtilRechercher = "Utilisateur Inexistant";
                        profilModel.NbAbonnement = 0;
                        ViewData["Rechercher"] = profilModel;
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

        public ActionResult Suivre(string nom, bool Abonne)
        {
            var photoFunBD = new PhotoFunBD();
            var profilModel = new ProfilModel();
            int retour;
            
            if (Abonne)
            {
                photoFunBD.SupprimerRelAbonnement(nom, User.Identity.Name);
            }
            else
            {
                photoFunBD.AbonnerUtil(User.Identity.Name, nom);
                profilModel.Abonner = true;
            }

            profilModel.IdUtilRechercher = nom;
            if (photoFunBD.CompteNbAbonnement(profilModel, out retour))
            {
                profilModel.NbAbonnement = retour;
            }
            
            ViewData["Rechercher"] = profilModel;
            return View("ProfilUtil");
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
