﻿using System.Linq;
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
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Umbraco.Web;
using Umbraco.Web.WebApi;
using FluentValidation;
using FluentValidation.WebApi;
using UmbracoFood.Filters;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Repositories;
using UmbracoFood.Validators;

namespace UmbracoFood
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            RegisterServices(builder);
            RegisterRepositories(builder);
            RegisterValidators(builder);
            RegisterModelMappers(builder);

            builder.RegisterControllers(typeof(Global).Assembly);
            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterApiControllers(typeof(Global).Assembly);

            builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            builder.RegisterType<WebApiExceptionFilter>()
                .AsWebApiExceptionFilterFor<UmbracoApiController>()
                .InstancePerRequest();

            builder.RegisterType<DatabaseProvider>()
                .As<IDatabaseProvider>();

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new AutofacControllerActivator(container));
            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterModelMappers(ContainerBuilder builder)
        {
            var businessLogic = Assembly.Load("UmbracoFood.Infrastructure");

            builder.RegisterAssemblyTypes(businessLogic)
                .AsClosedTypesOf(typeof(IModelMapper<,>)).AsImplementedInterfaces();
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

        private static void RegisterValidators(ContainerBuilder builder)
        {
            var businessLogic = Assembly.Load("UmbracoFood");

            builder.RegisterAssemblyTypes(businessLogic)
               .Where(t => t.Name.EndsWith("Validator"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            builder.RegisterType<FluentValidationModelValidatorProvider>().As<System.Web.Http.Validation.ModelValidatorProvider>();
            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();
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