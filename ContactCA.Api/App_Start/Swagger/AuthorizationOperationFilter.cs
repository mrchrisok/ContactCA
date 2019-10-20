using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace ContactCA.Api.App_Start
{
   public class AuthorizationOperationFilter : IOperationFilter
   {
      public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
      {
         if (operation.parameters == null)
         {
            operation.parameters = new List<Parameter>();
         }

         operation.parameters.Add(new Parameter
         {
            name = "authorization",
            @in = "header",
            description = "access token",
            required = false,
            type = "string"
         });
      }
   }
}