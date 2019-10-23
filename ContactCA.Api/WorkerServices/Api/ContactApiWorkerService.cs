using AppCore;
using ContactCA.Data.DataModels;
using ContactCA.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace ContactCA.Api.WorkerServices
{
   public class ContactApiWorkerService : WorkerServiceBase, IContactApiWorkerService
   {
      public ContactApiWorkerService(IComponentResolver resolver)
         : base(resolver)
      {

      }

      public ContactDataModel GetContactDataModel(Contact contact)
      {
         return new ContactDataModel(contact);
      }

      public ContactDataModel VerifyReCaptchaResponse(ContactDataModel model)
      {
         try
         {
            using (var webClient = new HttpClient())
            {
               var reCaptchaVerifyParams = new Dictionary<string, string>
               {
                  {"secret", ConfigurationManager.AppSettings["reCapthchaSecretKey"] },
                  {"response", model.RecaptchaResponse }
               };

               //webClient.BaseAddress = new Uri("https://www.google.com/recaptcha/api/siteverify");
               var content = new StringContent(JsonConvert.SerializeObject(reCaptchaVerifyParams), Encoding.UTF8, "application/json");
               var postData = webClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", content).Result;
               postData.EnsureSuccessStatusCode();
            }

            return model;
         }
         catch (Exception ex)
         {
            return null;
         }
      }
   }

   public interface IContactApiWorkerService
   {
      ContactDataModel GetContactDataModel(Contact contact);
      ContactDataModel VerifyReCaptchaResponse(ContactDataModel model);
   }
}