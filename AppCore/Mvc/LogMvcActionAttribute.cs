using System.Web.Mvc;

namespace AppCore.Mvc
{
   public class LogMvcActionAttribute : ActionFilterAttribute
   {
      public ILogger _Logger { private get; set; }
      public override void OnActionExecuted(ActionExecutedContext filterContext)
      {
         base.OnActionExecuted(filterContext);
         //

         _Logger.Log(filterContext.ActionDescriptor.ActionName);
      }
   }
}