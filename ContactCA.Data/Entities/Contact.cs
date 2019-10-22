using ContactCA.Data.Repositories.Contracts;
using Newtonsoft.Json;
using System;

namespace ContactCA.Data.Entities
{
   public class Contact : EntityBase, IIdentifiableEntity
   {
      public Contact()
      {

      }

      public Guid ContactID { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string EmailAddress { get; set; }
      public string Telephone { get; set; }
      public string Message { get; set; }
      public DateTime BestTimeToCall { get; set; }

      #region Members.IIdentifiableEntity
      [JsonIgnore]
      Guid IIdentifiableEntity.EntityID
      {
         get { return ContactID; }
         set { ContactID = value; }
      }

      #endregion
   }
}
