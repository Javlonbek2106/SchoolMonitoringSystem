using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace SchoolMonitoringSystem.Application.RateLimiting
{
    public static class ConfigurationServices
    {
        public static void AddRateLimiters(WebApplicationBuilder builder)
        {
            builder.Services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 10,
                            QueueLimit = 5,
                            Window = TimeSpan.FromSeconds(10)
                        }));
            });


            builder.Services.AddRateLimiter(options =>
            {
                options.AddTokenBucketLimiter("Token", c =>
                {
                    c.TokenLimit = 8;
                    c.QueueLimit = 0;
                    c.TokensPerPeriod = 2;
                    c.ReplenishmentPeriod = TimeSpan.FromSeconds(20);
                });
            });


            builder.Services.AddRateLimiter(options =>
            {
                options.AddSlidingWindowLimiter("Sliding", s =>
                {
                    s.PermitLimit = 8;
                    s.QueueLimit = 0;
                    s.AutoReplenishment = false;
                    s.SegmentsPerWindow = 2;
                    s.Window = TimeSpan.FromSeconds(50);
                });
            });

        }
    }
}
