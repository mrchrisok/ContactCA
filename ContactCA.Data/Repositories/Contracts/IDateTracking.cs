using System;

namespace ContactCA.Data.Repositories.Contracts
{
   public interface IDateTracking
   {
      DateTime? DateCreated { get; set; }
      DateTime? DateModified { get; set; }
   }
}
