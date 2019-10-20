using AppCore;
using ContactCA.Api.Models;
using ContactCA.Data.Entities;
using ContactCA.Data.Repositories.Contracts;
using System.Linq;
using System.Web.Http;

namespace ContactCA.Api.Controllers
{
   [Authorize]
   [RoutePrefix("api/contact")]
   public class ContactController : ApiController
   {
      public ContactController(IComponentResolver iocContainer)
      {
         // set up di

         _container = iocContainer;
         _contactRepository = _container.Resolve<IContactRepository>();
      }

      protected IComponentResolver _container;
      protected IContactRepository _contactRepository;

      #region GET methods

      [HttpGet]
      [Route("get")]
      public Contact[] GetContacts()
      {
         //var userId = RequestContext.Principal.Identity.GetUserId();
         //return new string[] { "value1", "value2", userId };

         return _contactRepository.GetContacts().ToArray();
      }

      [HttpGet]
      [Route("get/{email}")]
      public Contact GetContact(string email)
      {
         return _contactRepository.GetContact(email);
      }

      [HttpGet]
      [Route("get/{id}")]
      public Contact GetContactById(int id)
      {
         return _contactRepository.GetContactById(id);
      }

      [HttpGet]
      [Route("get/{firstName}")]
      public Contact[] GetContactsByFirstName(string firstName)
      {
         return _contactRepository.GetContactsByFirstName(firstName).ToArray();
      }

      [HttpGet]
      [Route("get/{lastName}")]
      public Contact[] GetContactsByLastName(string lastName)
      {
         return _contactRepository.GetContactsByLastName(lastName).ToArray();
      }

      #endregion

      [AllowAnonymous]
      [HttpPost]
      [Route("add")]
      public ContactViewModel AddContact([FromBody]ContactViewModel contact)
      {
         contact.BestCallDateTime = contact.BestCallDate.Date.Add(contact.BestCallTime.TimeOfDay);

         var savedContact = _contactRepository.Add(contact);

         var model = new ContactViewModel();
         SimpleMapper.MapProperties(savedContact, model);

         return model;
      }

      [HttpPut]
      [Route("update")]
      public Contact UpdateContact([FromBody]Contact contact)
      {
         return _contactRepository.Update(contact);
      }

      [HttpPut]
      [Route("delete/{id}")]
      public void DeleteContact(int id)
      {
         _contactRepository.Remove(id);
      }

      [HttpPut]
      [Route("delete")]
      public void DeleteContact([FromBody]Contact contact)
      {
         _contactRepository.Remove(contact);
      }
   }
}
