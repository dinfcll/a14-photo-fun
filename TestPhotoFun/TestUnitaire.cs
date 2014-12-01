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
            rm.UserName = "Mouissa";
            //when
            var result = requeteutilBD.InsererUtil(rm);
            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestExtraireUtil()
        {
            //given
            var requeUtilBD = new RequeteUtilBD();
            //when
            var result = requeUtilBD.ExtraireUtil("Mouissa");
            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEnregistrerPhoto()
        {
            //given
            var requetePhotoBD = new RequetePhotoBD();
            var photoModel = new PhotoModels();
            photoModel.util = "Mouissa";
            photoModel.Categorie = "Autres";;
            photoModel.image = photoModel.util + "_Bateau" + photoModel.IDUniqueNomPhoto;
            //when
            var result = requetePhotoBD.EnregistrerPhoto(photoModel);
            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestAbonnerUtil()
        {
            //given
            var requeteAbonnementUtilBD = new RequeteAbonnementUtilBD();
            //when
            var result = requeteAbonnementUtilBD.AbonnerUtil("Mouissa", "nicolo");
            //then
            Assert.IsTrue(result);
        }
    }
}
