using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DakiHunt.DataAccess.Interfaces.Service;
using DakiHunt.DataAccess.Services;

namespace DakiHunt.Api.Composition
{
    internal static class ResourceLocator
    {
        private static IContainer _container;

        public static ILifetimeScope ObtainLifetimeScope() => _container.BeginLifetimeScope();

        public static void RegisterDependencies(this ContainerBuilder builder)
        {
            builder.RegisterBuildCallback(BuildCallback);

            builder.RegisterType<UserService>().As<IUserService>();

        }

        private static void BuildCallback(IContainer obj)
        {
            _container = obj;
        }
    }
}
