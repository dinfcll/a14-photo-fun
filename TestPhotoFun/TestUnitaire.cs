using System;
using System.Web.Mvc;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoFun.Controllers;
using PhotoFun.Models;

namespace TestPhotoFun
{
    [TestClass]
    public class TestUnitaire
    {
        [TestMethod]
        public void TestRetourneLaVueSelonLaCategorie()
        {
            //given
            var controller = new HomeController();
            //when
            var result = controller.RetourneLaVueSelonCategorie("Autres") as ActionResult;
            //then
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestControllerPhotoUtilUnParametre()
        {
            //given
            var controller = new AccountController();
            //when
            var result = controller.PhotoUtil("UnNom") as ActionResult;
            //then
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestControllerProfilUtilUnParametre()
        {
            //given
            var controller = new AccountController();
            //when
            var result = controller.ProfilUtil("UnNom") as ActionResult;
            //then
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAjoutUtilBD()
        {
            //given
            var requeteutilBD = new RequeteUtilBD();
            var rm = new RegisterModel();
            rm.Courriel = "nic@hotmail.com";
            rm.NomUtil = "Pandolfo";
            rm.PrenomUtil = "Nicolas";
            rm.UserName = "nicpanal";
            //when
            var result = requeteutilBD.InsererUtil(rm);
            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEnregistrerPhoto()
        {
            //Given
            var requetePhotoBD = new RequetePhotoBD();
            var photoModel = new PhotoModels();
            photoModel.Categorie = "Animaux";
            photoModel.Commentaires = "Gros jambon";
            photoModel.util = "nicpanal";
            photoModel.image = photoModel.util + "_Allo" + photoModel.IDUniqueNomPhoto;
            //when
            var result = requetePhotoBD.EnregistrerPhoto(photoModel);
            //then
            Assert.IsTrue(result);
        }
    }
}
