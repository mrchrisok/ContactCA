using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Http;

namespace ContactCA.Api.Controllers
{
   [Authorize]
   public class ContactController : ApiController
   {
      public ContactController()
      {
         // set up di
      }

      // GET api/values
      public IEnumerable<string> Get()
      {
         var userId = RequestContext.Principal.Identity.GetUserId();
         return new string[] { "value1", "value2", userId };
      }

      // GET api/values/5
      public string Get(int id)
      {
         return "value";
      }

      // POST api/values
      public void Post([FromBody]string value)
      {
      }

      // PUT api/values/5
      public void Put(int id, [FromBody]string value)
      {
      }

      // DELETE api/values/5
      public void Delete(int id)
      {
      }
   }
}
