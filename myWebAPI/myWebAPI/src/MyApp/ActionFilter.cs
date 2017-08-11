using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyApp
{
    public class ActionFilter : ActionFilterAttribute
    {
        const string actionKey = "action";
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var stopwatch = new Stopwatch();
            actionContext.Request.Properties[actionKey] = stopwatch;

            var actionName = actionContext.ActionDescriptor.ActionName;
            var controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;

            var logger = (INLogger)actionContext.Request.GetDependencyScope().GetService(typeof(INLogger));
            logger.Info($"{controllerName} {actionName} start watch");

            stopwatch.Start();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var stopwatch = actionExecutedContext.Request.Properties[actionKey] as Stopwatch;
            if (stopwatch != null)
            {
                stopwatch.Stop();

                var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
                var controllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;

                var logger = (INLogger)actionExecutedContext.Request.GetDependencyScope().GetService(typeof(INLogger));
                logger.Info($"{controllerName} {actionName} stop watch {stopwatch.Elapsed.TotalMilliseconds} ms");
            }
        }
    }
}