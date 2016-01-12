using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Umbraco.Web;

namespace UmbracoFood
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            RegisterServices(builder);
            RegisterRepositories(builder);


            builder.RegisterControllers(typeof (Global).Assembly);
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);
            
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        
        private static void RegisterServices(ContainerBuilder builder)
        {
            var businessLogic = Assembly.Load("UmbracoFood.Services");

            builder.RegisterAssemblyTypes(businessLogic)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();            
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            var businessLogic = Assembly.Load("UmbracoFood.Infrastructure2");

            builder.RegisterAssemblyTypes(businessLogic)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
        }
    }
}