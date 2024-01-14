// В проекте Infrastructure.RateLimit
using Infrastructure.RateLimit;

using Microsoft.AspNetCore.Builder;

public static class RateLimitExtensions
{
    public static IApplicationBuilder UseRateLimiter(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RateLimitMiddleware>();
    }
}
