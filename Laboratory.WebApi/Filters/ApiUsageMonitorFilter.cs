using System;
using System.Collections.Concurrent;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Laboratory.WebApi.Filters
{
    /// <summary>
    /// 注意：此处需要在WebApiConfig.cs中注册，因为FilterConfig.cs只能注册Mvc命名空间下的IActionFilter
    /// </summary>
    public class ApiUsageMonitorFilter : ActionFilterAttribute, IActionFilter
    {
        private static readonly ConcurrentDictionary<string, int> _reqCount = new ConcurrentDictionary<string, int>(StringComparer.Ordinal);

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var reqPath = actionContext.Request.RequestUri.LocalPath;
            var reqMethod = actionContext.Request.Method;

            _reqCount.AddOrUpdate(reqPath, 1, (a, b) =>
            {
                return b + 1;
            });

            if (_reqCount.Count % 2000 == 0)
            {
                //TODO Log.
                _reqCount.Clear();
            }

            base.OnActionExecuting(actionContext);
        }
    }
}