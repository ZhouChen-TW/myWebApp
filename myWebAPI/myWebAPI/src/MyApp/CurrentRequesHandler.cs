using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp
{
    public class CurrentRequesHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var logger = (INLogger)request.GetDependencyScope().GetService(typeof(INLogger));
            logger.Info($"{request.RequestUri} start watch");

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var responseMessage = base.SendAsync(request, cancellationToken).Result;

            stopWatch.Stop();
            logger.Info($"{request.RequestUri} stop watch {stopWatch.Elapsed.TotalMilliseconds} ms");

            return Task.FromResult(responseMessage);
        }
    }
}