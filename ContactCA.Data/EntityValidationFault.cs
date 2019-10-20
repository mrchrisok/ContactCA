using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ContactCA.Data
{
   public class EntityValidationFault
   {
      #region Declarations
      private readonly IEnumerable<ValidationResult> _validationErrors;
      #endregion

      #region Properties
      /// <summary>
      /// A collection of validation errors for the entity that failed validation.
      /// </summary>
      //[IgnoreDataMember]
      public IEnumerable<ValidationResult> ValidationErrors
      {
         get
         {
            return _validationErrors.ToList();
         }
      }

      //[DataMember]
      public string Message { get; set; }
      #endregion

      #region Constructors
      /// <summary>
      /// Initializes a new instance of the EntityValidationFault class.
      /// </summary>
      public EntityValidationFault()
      {
      }

      /// <summary>
      /// Initializes a new instance of the EntityValidationFault class.
      /// </summary>
      /// <param name="message">The error message for this exception.</param>
      public EntityValidationFault(string message)
      {
         Message = message;
      }

      /// <summary>
      /// Initializes a new instance of the EntityValidationException class.
      /// </summary>
      /// <param name="message">The error message for this exception.</param>
      /// <param name="validationErrors">A collection of validation errors.</param>
      public EntityValidationFault(string message, IEnumerable<ValidationResult> validationErrors)
      {
         Message = message;
         _validationErrors = validationErrors;
      }
      #endregion
   }
}
