using AppCore;
using ContactCA.Api.Models;

namespace ContactCA.Api.WorkerServices
{
   public class HomeWorkerService : WorkerServiceBase, IHomeWorkerService
   {
      public HomeWorkerService(IComponentResolver resolver)
         : base(resolver)
      {
      }

      public IHomeViewModel GetHomeViewModel()
      {
         var model = _resolver.Resolve<IHomeViewModel>();

         // start all as shown
         foreach (var propertyInfo in model.GetType().GetProperties())
            if (propertyInfo.PropertyType.Equals(typeof(bool)) && propertyInfo.Name.StartsWith("Show"))
               propertyInfo.SetValue(model, true);

         // turn these off
         model.ShowAll = false;
         model.ShowPortfolio = false;
         model.ShowClients = false;
         model.ShowAbout = false;

         return model;
      }
   }

   public interface IHomeWorkerService
   {
      IHomeViewModel GetHomeViewModel();
   }
}