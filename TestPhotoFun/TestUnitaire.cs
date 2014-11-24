using System;
using System.Web.Mvc;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoFun.Controllers;

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
    }
}
