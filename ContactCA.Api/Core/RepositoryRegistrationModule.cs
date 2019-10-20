using Autofac;
using ContactCA.Data.Repositories;
using System.Linq;

namespace ContactCA.Api.Core
{
   public class RepositoryRegistrationModule : Autofac.Module
   {
      // only using this as a reminder that autofac has these
      // personally, prefer to di the repositories in Global GetContainer to keep di in one place

      protected override void Load(ContainerBuilder builder)
      {
         builder.RegisterAssemblyTypes(typeof(ContactRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .As(t => t.GetInterfaces()?.FirstOrDefault(i => i.Name == "I" + t.Name))
            .InstancePerRequest();
      }
   }
}