using AppCore;
using ContactCA.Data.Entities;
using ContactCA.Data.Repositories.Contracts;
using System;
using System.Linq;
using System.Web.Http;

namespace ContactCA.Api.Controllers
{
   [Authorize]
   [RoutePrefix("api/contact")]
   public class ContactController : ApiController
   {
      public ContactController(IComponentResolver resolver)
      {
         // set up di

         _resolver = resolver;
         _contactRepository = _resolver.Resolve<IContactRepository>();
      }

      protected IComponentResolver _resolver;
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
      public Contact GetContactById(string id)
      {
         return _contactRepository.GetContactById(new Guid(id));
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
      public Contact AddContact([FromBody]Contact contact)
      {
         var savedContact = _contactRepository.Add(contact);

         return savedContact;
      }

      [HttpPut]
      [Route("update")]
      public Contact UpdateContact([FromBody]Contact contact)
      {
         return _contactRepository.Update(contact);
      }

      [HttpPut]
      [Route("delete/{id}")]
      public void DeleteContact(string id)
      {
         _contactRepository.Remove(new Guid(id));
      }

      [HttpPut]
      [Route("delete")]
      public void DeleteContact([FromBody]Contact contact)
      {
         _contactRepository.Remove(contact);
      }
   }
}
