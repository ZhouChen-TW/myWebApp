using System;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace MyApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var httpConfiguration = GlobalConfiguration.Configuration;
            var containerBuilder = new ContainerBuilder();
            new BootStrap(containerBuilder).Initialize(httpConfiguration);
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(containerBuilder.Build());
        }
    }
}