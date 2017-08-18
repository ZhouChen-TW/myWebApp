using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp
{
    public class CurrentRequestHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var logger = (INLogger)request.GetDependencyScope().GetService(typeof(INLogger));
            logger.Info($"{request.RequestUri} start watch");

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            return base.SendAsync(request, cancellationToken).ContinueWith(
                t =>
                {
                    stopWatch.Stop();
                    logger.Info($"{request.RequestUri} stop watch {stopWatch.Elapsed.TotalMilliseconds} ms");
                    return t.Result;
                },
                TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}