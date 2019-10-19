using ContactCA.Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ContactCA.Api.Tests.Controllers
{
   [TestClass]
   public class ContactControllerTest
   {
      [TestMethod]
      public void Get()
      {
         // Arrange
         ContactController controller = new ContactController();

         // Act
         IEnumerable<string> result = controller.Get();

         // Assert
         Assert.IsNotNull(result);
         Assert.AreEqual(2, result.Count());
         Assert.AreEqual("value1", result.ElementAt(0));
         Assert.AreEqual("value2", result.ElementAt(1));

      }

      [TestMethod]
      public void GetById()
      {
         // Arrange
         ContactController controller = new ContactController();

         // Act
         string result = controller.Get(5);

         // Assert
         Assert.AreEqual("value", result);
      }

      [TestMethod]
      public void Post()
      {
         // Arrange
         ContactController controller = new ContactController();

         // Act
         controller.Post("value");

         // Assert
      }

      [TestMethod]
      public void Put()
      {
         // Arrange
         ContactController controller = new ContactController();

         // Act
         controller.Put(5, "value");

         // Assert
      }

      [TestMethod]
      public void Delete()
      {
         // Arrange
         ContactController controller = new ContactController();

         // Act
         controller.Delete(5);

         // Assert
      }
   }
}
