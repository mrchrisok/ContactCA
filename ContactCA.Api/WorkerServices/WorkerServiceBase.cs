using AppCore;

namespace ContactCA.Api.WorkerServices
{
   public interface IWorkerService
   {

   }

   public abstract class WorkerServiceBase : IWorkerService
   {
      public WorkerServiceBase(IComponentResolver resolver)
      {
         _resolver = resolver;
      }

      protected IComponentResolver _resolver;
   }
}