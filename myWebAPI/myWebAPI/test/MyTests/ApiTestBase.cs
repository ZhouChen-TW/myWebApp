using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using MyApp;

namespace MyTests
{
    public class ApiTestBase : IDisposable
    {
        readonly HttpConfiguration httpConfiguration = new HttpConfiguration();
        HttpClient httpClient;
        HttpServer httpServer;

        protected void RegisterFakeInstance(Action<ContainerBuilder> action)
        {
            new BootStrap{OnBuildContainerBuilder = action}.Initialize(httpConfiguration);
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
