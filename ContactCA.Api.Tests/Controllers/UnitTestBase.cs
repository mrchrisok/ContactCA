using AppCore;
using Autofac;
using ContactCA.Api.Controllers;
using ContactCA.Api.Models;
using ContactCA.Api.WorkerServices;
using ContactCA.Data;
using ContactCA.Data.DataModels;
using ContactCA.Data.Entities;
using ContactCA.Data.Repositories.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;

namespace ContactCA.Api.Tests.Controllers
{
   public class UnitTestBase
   {
      private IContainer _container;
      protected IContainer Container
      {
         get
         {
            if (_container == null)
            {
               var builder = new ContainerBuilder();

               builder.RegisterType<ComponentResolver>().As<IComponentResolver>().SingleInstance();

               // db context .. if needed
               builder.RegisterType<ContactCADbContext>().As<ContactCADbContext>().InstancePerLifetimeScope();

               // Repositories
               builder.RegisterInstance(GetMockContactRepository());
               builder.RegisterInstance(GetMockHomeViewModel());

               // worker services
               builder.RegisterInstance(GetMockHomeWorkerService());
               builder.RegisterInstance(GetContactApiWorkerService());

               // Register the CompanyDataRepository for property injection not constructor allowing circular references
               //builder.RegisterType<ContactRepository>().As<IContactRepository>()
               //       .InstancePerLifetimeScope()
               //       .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

               // register controllers
               builder.RegisterType<ContactController>().As<IHttpController>().InstancePerLifetimeScope();

               var container = builder.Build();

               _container = container;
            }

            return _container;
         }
      }

      protected ContactCADbContext ContactCADbContext
      {
         get
         {
            return Container.Resolve<ContactCADbContext>();
         }
      }

      protected IHomeViewModel GetMockHomeViewModel()
      {
         var mockHomeViewModel = new Mock<IHomeViewModel>();
         mockHomeViewModel.Setup(x => x.ShowAll).Returns(true);
         mockHomeViewModel.Setup(x => x.ShowClients).Returns(false);

         return mockHomeViewModel.Object;
      }

      protected IHomeWorkerService GetMockHomeWorkerService()
      {
         var mockHomeViewModel = new Mock<IHomeViewModel>();
         mockHomeViewModel.Setup(x => x.ShowAll).Returns(false);
         mockHomeViewModel.Setup(x => x.ShowClients).Returns(false);
         //

         var mockHomeWorkerService = new Mock<IHomeWorkerService>();

         mockHomeWorkerService.Setup(x => x.GetHomeViewModel()).Returns(mockHomeViewModel.Object);

         return mockHomeWorkerService.Object;
      }
      protected IContactApiWorkerService GetContactApiWorkerService()
      {
         //var contact = new Contact() { FirstName = "Test", LastName = "Contact" };

         var mockContactApiWorkerService = new Mock<IContactApiWorkerService>();
         mockContactApiWorkerService.Setup(x => x.GetContactDataModel(It.IsAny<Contact>()))
            .Returns((Contact contact) => new ContactDataModel(contact));

         // fail all for now .. for a real project, can set up pass/fail scenarios
         mockContactApiWorkerService.Setup(x => x.VerifyReCaptchaResponse(It.IsAny<ContactDataModel>()))
            .Returns((ContactDataModel model) => null);

         return mockContactApiWorkerService.Object;
      }

      protected IContactRepository GetMockContactRepository()
      {
         // mock one up ...

         var mockContactRepository = new Mock<IContactRepository>();

         List<Contact> contacts = new List<Contact>()
         {
            new Contact()
            {
               ContactID = new Guid(),
               FirstName = "Mike",
               LastName = "Tyson",
               Telephone = "2223334444",
               BestTimeToCall = DateTime.UtcNow.AddDays(1).AddHours(1).AddMinutes(15),
               DateCreated = DateTime.UtcNow.AddDays(-3).AddHours(1).AddMinutes(15)
            },
            new Contact()
            {
               ContactID = new Guid(),
               FirstName = "Yogi",
               LastName = "Bear",
               Telephone = "2223334444",
               BestTimeToCall = DateTime.UtcNow.AddDays(1).AddHours(3).AddMinutes(30),
               DateCreated = DateTime.UtcNow.AddDays(-2).AddHours(3).AddMinutes(30)
            },
            new Contact()
            {
               ContactID = new Guid("C56A4180-65AA-42EC-A945-5FD21DEC0538"),
               FirstName = "Donald",
               LastName = "Trump",
               Telephone = "2223334444",
               BestTimeToCall = DateTime.UtcNow.AddDays(1).AddHours(1).AddMinutes(15),
               DateCreated = DateTime.UtcNow.AddDays(-1).AddHours(5).AddMinutes(45)
            }
         };

         mockContactRepository.Setup(x => x.GetContacts()).Returns(contacts);

         mockContactRepository.Setup(x => x.GetContactsByFirstName(It.IsAny<string>()))
            .Returns((string firstName) => contacts.Where(x => x.FirstName == firstName));

         mockContactRepository.Setup(x => x.GetContactById(It.IsAny<Guid>()))
            .Returns((Guid id) => contacts.FirstOrDefault(x => x.ContactID == id));


         return mockContactRepository.Object;
      }
   }
}
