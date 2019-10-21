using AppCore;
using Autofac;
using ContactCA.Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace ContactCA.Api.Tests.Controllers
{
   [TestClass]
   public class ContactControllerTest : UnitTestBase
   {

      [TestMethod]
      public void get_contacts_test()
      {
         // Arrange
         var componentResolver = Container.Resolve<IComponentResolver>();
         var controller = new ContactController(componentResolver);

         // Act
         var result = controller.GetContacts();

         // Assert
         Assert.IsNotNull(result);
         Assert.AreEqual(3, result.Count());
      }

      [TestMethod]
      public void get_contacts_by_firstname()
      {
         // Arrange
         var componentResolver = Container.Resolve<IComponentResolver>();
         var controller = new ContactController(componentResolver);

         // Act
         var result = controller.GetContactsByFirstName("Mike");

         // Assert
         Assert.AreEqual(1, result.Count());
      }

      [TestMethod]
      public void get_contact_by_id()
      {
         // Arrange
         var componentResolver = Container.Resolve<IComponentResolver>();
         var controller = new ContactController(componentResolver);

         var testGuid = new Guid("C56A4180-65AA-42EC-A945-5FD21DEC0538");

         // Act
         var result = controller.GetContactById(testGuid.ToString());

         // Assert
         Assert.IsNotNull(result);
         Assert.AreEqual("Trump", result.LastName);
      }

      #region Helpers

      HttpRequestMessage GetRequest()
      {
         HttpConfiguration config = new HttpConfiguration();
         HttpRequestMessage request = new HttpRequestMessage();
         request.Properties["MS_HttpConfiguration"] = config;
         return request;
      }

      T GetResponseData<T>(HttpResponseMessage result)
      {
         ObjectContent<T> content = result.Content as ObjectContent<T>;
         if (content != null)
         {
            T data = (T)(content.Value);
            return data;
         }
         else
            return default(T);
      }
      #endregion
   }
}
