using System.Reflection;
using System.Web.Http;
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using AutoMapper.Mappers;
using Umbraco.Web;
using Umbraco.Web.WebApi;
using FluentValidation;
using FluentValidation.WebApi;
using Umbraco.Core;
using UmbracoFood.Filters;
using UmbracoFood.Helpers;
using UmbracoFood.Infrastructure.Mapping;
using UmbracoFood.Infrastructure.Repositories;
using UmbracoFood.Interfaces;
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
            var assemblies =  AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.BaseType == typeof(Profile))
                .As<Profile>();

            builder.Register(ctx => new ConfigurationStore(new TypeMapFactory(), MapperRegistry.AllMappers()))
               .AsImplementedInterfaces()
               .SingleInstance()
               .OnActivating(x => {
                   foreach (var profile in x.Context.Resolve<IEnumerable<Profile>>())
                   {
                       x.Instance.AddProfile(profile);
                   }
               });
            var businessLogic = Assembly.Load("UmbracoFood.Infrastructure");
            builder.RegisterAssemblyTypes(businessLogic)
                .AsClosedTypesOf(typeof(IModelMapper<,>)).AsImplementedInterfaces();
            builder.Register<IMappingEngine>(c => Mapper.Engine);
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