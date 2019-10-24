using System.Web.Http;
using ContactCA.Api;
using ContactCA.Api.App_Start;
using Swashbuckle.Application;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ContactCA.Api
{
   public class SwaggerConfig
   {
      // see notes in the first check-in if additional swagger config is needed

      public static void Register()
      {
         var thisAssembly = typeof(SwaggerConfig).Assembly;

         GlobalConfiguration.Configuration
             .EnableSwagger(c =>
             {
                c.DocumentFilter<AuthTokenOperationFilter>();
                c.OperationFilter<AuthorizationOperationFilter>();

                // Use "SingleApiVersion" to describe a single version API. Swagger 2.0 includes an "Info" object to
                // hold additional metadata for an API. Version and title are required but you can also provide
                // additional fields by chaining methods off SingleApiVersion.
                //
                c.SingleApiVersion("v1", "ContactCA.Api");

                // If you want the output Swagger docs to be indented properly, enable the "PrettyPrint" option.
                //
                c.PrettyPrint();

                // In accordance with the built in JsonSerializer, Swashbuckle will, by default, describe enums as integers.
                // You can change the serializer behavior by configuring the StringToEnumConverter globally or for a given
                // enum type. Swashbuckle will honor this change out-of-the-box. However, if you use a different
                // approach to serialize enums as strings, you can also force Swashbuckle to describe them as strings.
                //
                c.DescribeAllEnumsAsStrings();
             })

             .EnableSwaggerUi(c =>
             {
                // Use the "DocumentTitle" option to change the Document title.
                // Very helpful when you have multiple Swagger pages open, to tell them apart.
                //
                c.DocumentTitle("ContactCA.Api");
             });
      }
   }
}
