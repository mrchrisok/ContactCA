using Autofac;

namespace AppCore
{
   public interface IComponentResolver
   {
      T Resolve<T>();
   }

   public class ComponentResolver : IComponentResolver
   {
      public ComponentResolver(ILifetimeScope container)
      {
         _Container = container;
      }

      ILifetimeScope _Container;

      public T Resolve<T>()
      {
         return _Container.Resolve<T>();
      }
   }
}