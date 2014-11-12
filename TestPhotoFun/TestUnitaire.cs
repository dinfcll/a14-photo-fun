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
        public void TestRetourneAuLoginSiUtilNonConnecteEtVeuxImporter()
        {
            //given
            var controller = new HomeController();
            //when
            var result = controller.Importer("TransfertEchoue") as ViewResult;
            //then
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
