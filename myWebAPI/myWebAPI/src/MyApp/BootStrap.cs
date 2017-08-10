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
        public static void Initialize(HttpConfiguration httpConfiguration)
        {
            IContainer container = CreateRootScope();
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            RegisterRoutes(httpConfiguration);
        }

        static void RegisterRoutes(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Routes.MapHttpRoute(
                "get message",
                "message",
                new {controller = "Message", action = "GetMessage"},
                new {httpMethod = new HttpMethodConstraint(HttpMethod.Get)});
        }

        static IContainer CreateRootScope()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterModules(containerBuilder);
            return containerBuilder.Build();
        }

        static void RegisterModules(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<MessageGenerate>().InstancePerLifetimeScope();
        }
    }
}