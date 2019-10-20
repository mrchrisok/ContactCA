using ContactCA.Data.Repositories.Contracts;
using System;
using System.Web.Http.Description;

namespace ContactCA.Data.Entities
{
   public abstract class EntityBase : IDateTracking
   {
      /// <summary>
      /// Gets or sets the date and time the object was created.
      /// </summary>
      [ApiExplorerSettings(IgnoreApi = true)]
      public DateTime? DateCreated { get; set; }

      /// <summary>
      /// Gets or sets the date and time the object was last modified.
      /// </summary>
      [ApiExplorerSettings(IgnoreApi = true)]
      public DateTime? DateModified { get; set; }
   }
}
