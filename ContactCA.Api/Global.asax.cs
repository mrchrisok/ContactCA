using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AppCore.Mvc;
using AppCore.WebApi;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Extensions.Configuration;

namespace ContactCA.Api
{
   public class WebApiApplication : System.Web.HttpApplication
   {
      protected void Application_Start()
      {
         AreaRegistration.RegisterAllAreas();
         GlobalConfiguration.Configure(WebApiConfig.Register);
         FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
         RouteConfig.RegisterRoutes(RouteTable.Routes);
         BundleConfig.RegisterBundles(BundleTable.Bundles);

         // DI setup
         var container = GetContainer();

         // resolve controllers from container
         DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
         GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

         // hook up filters
         GlobalFilters.Filters.Add(container.Resolve<LogMvcActionAttribute>());
         GlobalConfiguration.Configuration.Filters.Add(container.Resolve<LogWebApiActionAttribute>());
      }

      private IContainer GetContainer()
      {
         ContainerBuilder builder = new ContainerBuilder();

         // register controllers
         builder.RegisterControllers(typeof(WebApiApplication).Assembly)
            .PropertiesAutowired();
         builder.RegisterApiControllers(typeof(WebApiApplication).Assembly)
            .InstancePerRequest().PropertiesAutowired();

         // repositories .. done in module
         //builder.RegisterAssemblyTypes(typeof(ContactRepository).Assembly)
         //   .Where(t => t.Name.EndsWith("Repository"))
         //   .As(t => t.GetInterfaces()?.FirstOrDefault(i => i.Name == "I" + t.Name))
         //   .InstancePerRequest();

         var assembly = Assembly.GetExecutingAssembly();

         builder.RegisterAssemblyTypes(assembly)
            .Where(t => t.Name.EndsWith("WorkerService"))
            .AsImplementedInterfaces();

         builder.RegisterAssemblyTypes(assembly)
            .Where(t => t.Name.EndsWith("ViewModel"))
            .AsImplementedInterfaces();

         // register other types
         IConfigurationBuilder config = new ConfigurationBuilder();
         config.AddJsonFile("autofac.json");

         ConfigurationModule module = new ConfigurationModule(config.Build());

         // register the container itself .. as IComponentResolver
         builder.RegisterModule(module);

         // register custom filters
         builder.RegisterFilterProvider(); //mvc
         builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
         builder.RegisterType<LogMvcActionAttribute>().PropertiesAutowired();
         builder.RegisterType<LogWebApiActionAttribute>().PropertiesAutowired();

         IContainer container = builder.Build();

         return container;
      }
   }
}
