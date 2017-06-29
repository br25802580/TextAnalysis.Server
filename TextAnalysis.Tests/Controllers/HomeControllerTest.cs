using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAnalysis;
using TextAnalysis.Controllers;

namespace TextAnalysis.Tests.Controllers
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
            Assert.AreEqual("Text Analysis", result.ViewBag.Title);
        }
    }
}
