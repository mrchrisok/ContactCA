using AppCore;
using ContactCA.Api.Models;
using System.Configuration;

namespace ContactCA.Api.WorkerServices
{
   public class ContactWorkerService : WorkerServiceBase, IContactWorkerService
   {
      public ContactWorkerService(IComponentResolver resolver)
         : base(resolver)
      {
      }

      public IContactViewModel GetContactViewModel()
      {
         var model = _resolver.Resolve<IContactViewModel>();
         model.RecaptchaSiteKey = GetReCaptchaSiteKey();
         return model;
      }

      public string GetReCaptchaSiteKey()
      {
         return ConfigurationManager.AppSettings["reCaptchaSiteKey"];
      }
   }

   public interface IContactWorkerService
   {
      IContactViewModel GetContactViewModel();
      string GetReCaptchaSiteKey();
   }
}