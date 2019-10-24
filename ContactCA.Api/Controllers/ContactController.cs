using AppCore;
using ContactCA.Api.WorkerServices;
using ContactCA.Data.DataModels;
using ContactCA.Data.Entities;
using ContactCA.Data.Repositories.Contracts;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;

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
         _contactApiWorkerServcice = _resolver.Resolve<IContactApiWorkerService>();
      }

      protected IComponentResolver _resolver;
      protected IContactRepository _contactRepository;
      protected IContactApiWorkerService _contactApiWorkerServcice;

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
      public IHttpActionResult AddContact([FromBody]ContactDataModel model)
      {
         // todo: api docs should reflect that the RecaptchaResponse is not emitted

         var verifiedModel = _contactApiWorkerServcice.VerifyReCaptchaResponse(model);

         if (verifiedModel == null)
            return new InternalServerErrorResult(Request);
         else
         {
            var contactRepository = _resolver.Resolve<IContactRepository>();
            var savedContact = contactRepository.Add(model.Contact);

            return Ok(_contactApiWorkerServcice.GetContactDataModel(savedContact));
         }
      }

      [HttpPut]
      [Route("update")]
      public ContactDataModel UpdateContact([FromBody]ContactDataModel model)
      {
         var updatedContact = _contactRepository.Update(model.Contact);

         return _contactApiWorkerServcice.GetContactDataModel(updatedContact);
      }

      [HttpPut]
      [Route("delete/{id}")]
      public void DeleteContact(string id)
      {
         _contactRepository.Remove(new Guid(id));
      }
   }
}
