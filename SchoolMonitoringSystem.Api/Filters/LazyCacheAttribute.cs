using LazyCache;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SchoolMonitoringSystem.Api.Filters
{
    public class LazyCacheAttribute : ActionFilterAttribute
    {
        private static IAppCache cache;
        private string cacheKey;
        private readonly int _slidingTime;
        private readonly int _absoluteExpirationRelativeToNow;

        public LazyCacheAttribute(int slidingTime, int absoluteExpirationRelativeToNow)
        {
            _slidingTime = slidingTime;
            _absoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext executingContext, ActionExecutionDelegate next)
        {
            cache = executingContext.HttpContext.RequestServices.GetRequiredService<IAppCache>();
            cacheKey = executingContext.HttpContext.Request.Path;
            var res = await cache.GetOrAddAsync(cacheKey, c =>
            {
                c.SlidingExpiration = TimeSpan.FromSeconds(_slidingTime);
                c.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_absoluteExpirationRelativeToNow);
                return next();
            });
            if(res!=null)
            {
                executingContext.Result = res.Result;
            }
        }
    }
}
