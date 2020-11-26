using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rating_review_example;
using rating_review_example.Controllers;

namespace rating_review_example.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }

        [TestMethod]
        public void Rating()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Rating() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Rating Review Services", result.ViewBag.Title);
        }
    }
}
