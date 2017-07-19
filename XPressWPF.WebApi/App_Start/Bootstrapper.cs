using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using XPressWPF.WebApi.Services;

namespace XPressWPF.WebApi
{
    public class Bootstrapper
    {
        public static void Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            RegisterTypes(builder);

            IContainer container = builder.Build();
            AutofacWebApiDependencyResolver resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

        }

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<QueryReader>().As<IQueryReader>().SingleInstance();
        }
    }
}
