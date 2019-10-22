using ContactCA.Data.Entities;

namespace ContactCA.Data.DataModels
{
   public class ContactDataModel
   {
      public ContactDataModel()
      {
      }

      public ContactDataModel(Contact contact)
      {
         Contact = contact;
         RecaptchaResponse = null;
      }

      public Contact Contact { get; set; }
      public string RecaptchaResponse { get; set; }
   }
}
