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

        public BootStrap(ContainerBuilder containerBuilder)
        {
            this.containerBuilder = containerBuilder;
        }

        public void Initialize(HttpConfiguration httpConfiguration)
        {
            RegisterFilters(httpConfiguration);
            RegisterRoutes(httpConfiguration);
            BuilderContainers();
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

        void BuilderContainers()
        {
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterModules();
        }

         void RegisterModules()
        {
            containerBuilder.RegisterType<MessageGenerate>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<NLogger>().As<INLogger>().InstancePerLifetimeScope();
        }
    }
}