using AppCore;
using ContactCA.Api.WorkerServices;
using System.Web.Mvc;

namespace ContactCA.Api.Controllers
{
   public class HomeController : Controller
   {
      public HomeController(IComponentResolver resolver)
      {
         _resolver = resolver;
      }

      protected IComponentResolver _resolver;

      public ActionResult Index()
      {
         var homeWorkerService = _resolver.Resolve<IHomeWorkerService>();

         return View(homeWorkerService.GetHomeViewModel());
      }

      [ChildActionOnly]
      [HttpGet]
      public ActionResult Contact()
      {
         var contactWorkerService = _resolver.Resolve<IContactWorkerService>();

         var model = contactWorkerService.GetContactViewModel();

         return PartialView("~/Views/Home/_Contact.cshtml", model);
      }

      public ActionResult Swagger()
      {
         return Redirect("~/swagger");
      }
   }
}
