using System;

namespace ContactCA.Data.Repositories.Contracts
{
   public interface IIdentifiableEntity
   {
      Guid EntityID { get; set; }
   }
}
