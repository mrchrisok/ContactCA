using Autofac;
using ContactCA.Data.Repositories;
using System.Linq;

namespace ContactCA.Core
{
   public class RepositoryRegistrationModule : Autofac.Module
   {
      protected override void Load(ContainerBuilder builder)
      {
         builder.RegisterAssemblyTypes(typeof(ContactRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .As(t => t.GetInterfaces()?.FirstOrDefault(i => i.Name == "I" + t.Name))
            .InstancePerRequest();
         //.WithParameter(new TypedParameter(typeof(string), "easyBlog"));
      }
   }
}