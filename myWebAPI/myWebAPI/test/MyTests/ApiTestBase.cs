using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using MyApp;

namespace MyTests
{
    public class ApiTestBase : IDisposable
    {
        readonly HttpConfiguration httpConfiguration = new HttpConfiguration();
        readonly ContainerBuilder myBuilder = new ContainerBuilder();
        IContainer myContainer;
        HttpClient httpClient;
        HttpServer httpServer;

        public ApiTestBase()
        {
            new BootStrap(myBuilder).Initialize(httpConfiguration);
        }

        protected void RegisterFakeInstance(Action<ContainerBuilder> action)
        {
            action(myBuilder);
            myContainer = myBuilder.Build();
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(myContainer);
            httpServer = new HttpServer(httpConfiguration);
        }

        protected HttpClient CreateHttpClient()
        {
            httpClient = new HttpClient(httpServer)
            {
                BaseAddress = new Uri("http://www.haha.com")
            };

            return httpClient;
        }

        public void Dispose()
        {
            httpClient.Dispose();
            httpServer.Dispose();
            httpConfiguration.Dispose();
        }
    }
}
