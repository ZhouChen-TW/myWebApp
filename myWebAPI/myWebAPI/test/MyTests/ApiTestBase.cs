using System;
using System.Net.Http;
using System.Web.Http;
using MyApp;

namespace MyTests
{
    public class ApiTestBase : IDisposable
    {
        readonly HttpConfiguration httpConfiguration = new HttpConfiguration();
        HttpClient httpClient;
        readonly HttpServer httpServer;

        public ApiTestBase()
        {
            BootStrap.Initialize(httpConfiguration);
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
