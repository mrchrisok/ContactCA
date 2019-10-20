using ContactCA.Data.Repositories.Contracts;
using Newtonsoft.Json;
using System;

namespace ContactCA.Data.Entities
{
   public class Contact : EntityBase, IIdentifiableEntity
   {
      public int ContactID { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Email { get; set; }
      public string PhoneNumber { get; set; }
      public DateTime BestCallDateTime { get; set; }

      #region Members.IIdentifiableEntity
      [JsonIgnore]
      int IIdentifiableEntity.EntityID
      {
         get { return ContactID; }
         set { ContactID = value; }
      }

      #endregion
   }
}
