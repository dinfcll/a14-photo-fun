using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoFun.Controllers;
using PhotoFun.Models;

namespace TestPhotoFun
{
    [TestClass]
    public class TestUnitaire
    {
        private const string NomUsager="Mouissa";
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
            var rm = new RegisterModel
            {
                Courriel = "nic@hotmail.com",
                NomUtil = "Pandolfo",
                PrenomUtil = "Nicolas",
                UserName = NomUsager
            };
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
            var result = requeUtilBD.ExtraireUtil(NomUsager);
            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEnregistrerPhoto()
        {
            //given
            var requetePhotoBD = new RequetePhotoBD();
            var photoModel = new PhotoModels {Util = NomUsager, Categorie = "Autres"};
            photoModel.Image = photoModel.Util + "_Bateau" + photoModel.IdUniqueNomPhoto;
            //when
            var result = requetePhotoBD.EnregistrerPhoto(photoModel);
            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestAbonnerUtil()
        {
            //given
            var requeteAbonnementUtilBD = new RequeteAbonnementUtilBd();
            //when
            var result = requeteAbonnementUtilBD.AbonnerUtil(NomUsager, "nicolo");
            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSupprimerRelAbonnement()
        {
            //given
            var requeteAbonnementUtilBD = new RequeteAbonnementUtilBd();
            //when
            var result = requeteAbonnementUtilBD.SupprimerRelAbonnement("nicolo", NomUsager);
            //then
            Assert.IsTrue(result);
        }
    }
}
