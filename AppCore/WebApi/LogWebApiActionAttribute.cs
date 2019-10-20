using System.Web.Http.Filters;

namespace AppCore.WebApi
{
   public class LogWebApiActionAttribute : ActionFilterAttribute
   {
      public ILogger _Logger { private get; set; }
      public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
      {
         base.OnActionExecuted(actionExecutedContext);
         //

         _Logger.Log(actionExecutedContext.ActionContext.ActionDescriptor.ActionName);
      }
   }
}