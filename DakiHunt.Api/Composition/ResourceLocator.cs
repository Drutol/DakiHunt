﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DakiHunt.Api.BL;
using DakiHunt.Api.BL.Crawlers;
using DakiHunt.DataAccess;
using DakiHunt.DataAccess.Interfaces;
using DakiHunt.DataAccess.Interfaces.Service;
using DakiHunt.DataAccess.Services;
using DakiHunt.Interfaces;

namespace DakiHunt.Api.Composition
{
    internal static class ResourceLocator
    {
        private static IContainer _container;

        public static ILifetimeScope ObtainLifetimeScope() => _container.BeginLifetimeScope();

        public static void RegisterDependencies(this ContainerBuilder builder)
        {
            builder.RegisterBuildCallback(BuildCallback);

            //Services
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<HuntService>().As<IHuntService>();
            builder.RegisterType<UserPropertiesService>().As<IAccountPropertiesService>();

            //Singletons
            builder.RegisterType<DomainMonitor>().As<IDomainMonitor>().SingleInstance();
           
            builder.RegisterType<DbInitializer>().As<IDbInitializer>().SingleInstance();
            //Crawlers
            builder.RegisterType<SurugayaCrawler>().As<IDomainCrawler>().SingleInstance();
            builder.RegisterType<SurugayaSearchCrawler>().As<IDomainSearchCrawler>().SingleInstance();

        }

        private static void BuildCallback(IContainer obj)
        {
            _container = obj;
        }
    }
}
