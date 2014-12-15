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
        public void TestAjoutUtilBd()
        {
            //given
            var requeteutilBd = new RequeteUtilBd();
            var rm = new RegisterModel
            {
                Courriel = "nic@hotmail.com",
                NomUtil = "Pandolfo",
                PrenomUtil = "Nicolas",
                UserName = NomUsager
            };
            //when
            var result = requeteutilBd.InsererUtil(rm);
            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestExtraireUtil()
        {
            //given
            var requeUtilBd = new RequeteUtilBd();
            //when
            var result = requeUtilBd.ExtraireUtil(NomUsager);
            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestEnregistrerPhoto()
        {
            //given
            var requetePhotoBd = new RequetePhotoBd();
            var photoModel = new PhotoModels {Util = NomUsager, Categorie = "Autres"};
            photoModel.Image = photoModel.Util + "_Bateau" + photoModel.IdUniqueNomPhoto;
            //when
            var result = requetePhotoBd.EnregistrerPhoto(photoModel);
            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestAbonnerUtil()
        {
            //given
            var requeteAbonnementUtilBd = new RequeteAbonnementUtilBd();
            //when
            var result = requeteAbonnementUtilBd.AbonnerUtil(NomUsager, "nicolo");
            //then
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSupprimerRelAbonnement()
        {
            //given
            var requeteAbonnementUtilBd = new RequeteAbonnementUtilBd();
            //when
            var result = requeteAbonnementUtilBd.SupprimerRelAbonnement("nicolo", NomUsager);
            //then
            Assert.IsTrue(result);
        }
    }
}
