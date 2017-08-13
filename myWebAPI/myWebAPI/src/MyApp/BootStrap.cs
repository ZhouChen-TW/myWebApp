using System;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Routing;
using Autofac;
using Autofac.Integration.WebApi;

namespace MyApp
{
    public class BootStrap
    {
        readonly ContainerBuilder containerBuilder;

        public BootStrap()
        {
            containerBuilder = new ContainerBuilder();
        }

        public Action<ContainerBuilder> OnBuildContainerBuilder { get; set; }
        public void Initialize(HttpConfiguration httpConfiguration)
        {
            RegisterFilters(httpConfiguration);
            RegisterRoutes(httpConfiguration);
            BuilderContainers(httpConfiguration);
        }

        static void RegisterFilters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Filters.Add(new ActionFilter());
        }

        static void RegisterRoutes(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Routes.MapHttpRoute(
                "get message by id",
                "message/{id}",
                new { controller = "Message", action = "GetMessageById" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get), id=@"\d+"});

            httpConfiguration.Routes.MapHttpRoute(
                "get message",
                "message",
                new {controller = "Message", action = "GetMessage"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)});
        }

        void BuilderContainers(HttpConfiguration httpConfiguration)
        {
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterModules();
            OnBuildContainerBuilder(containerBuilder);
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(containerBuilder.Build());
        }

        void RegisterModules()
        {
            containerBuilder.RegisterType<MessageGenerate>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<NLogger>().As<INLogger>().InstancePerLifetimeScope();
        }
    }
}