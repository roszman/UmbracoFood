using System.Reflection;
using System.Web.Http;
﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Umbraco.Web;
using Umbraco.Web.WebApi;
using UmbracoFood.Infrastructure.Filters;

namespace UmbracoFood
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            RegisterServices(builder);
            RegisterRepositories(builder);


            builder.RegisterControllers(typeof(Global).Assembly);
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterApiControllers(typeof(Global).Assembly);

            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            builder.RegisterType<WebApiExceptionFilter>()
                .AsWebApiExceptionFilterFor<UmbracoApiController>()
                .InstancePerRequest();

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;


            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator),
    new AutofacControllerActivator(container));

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
            var businessLogic = Assembly.Load("UmbracoFood.Infrastructure");

            builder.RegisterAssemblyTypes(businessLogic)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
        }

        private sealed class AutofacControllerActivator : IHttpControllerActivator
        {
            private readonly IContainer _container;

            public AutofacControllerActivator(IContainer container)
            {
                _container = container;
            }

            [DebuggerStepThrough]
            public IHttpController Create(HttpRequestMessage request,
                HttpControllerDescriptor controllerDescriptor, Type controllerType)
            {
                return (IHttpController)_container.Resolve(controllerType);
            }
        }

    }
}