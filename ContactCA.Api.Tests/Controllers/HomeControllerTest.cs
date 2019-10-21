using AppCore;
using Autofac;
using ContactCA.Api.Controllers;
using ContactCA.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace ContactCA.Api.Tests.Controllers
{
   [TestClass]
   public class HomeControllerTest : UnitTestBase
   {
      [TestMethod]
      public void Index()
      {
         // Arrange
         var resolver = Container.Resolve<IComponentResolver>();
         var controller = new HomeController(resolver);

         // Act
         ViewResult result = controller.Index() as ViewResult;

         // Assert
         Assert.IsNotNull(result);
         Assert.IsTrue(result.Model is IHomeViewModel);
      }
   }
}
