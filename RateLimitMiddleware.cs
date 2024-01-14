// В проекте Infrastructure.RateLimit
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConnectionMultiplexer _redis;

    public RateLimitMiddleware(RequestDelegate next, IConnectionMultiplexer redis)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _redis = redis ?? throw new ArgumentNullException(nameof(redis));
    }

    public async Task Invoke(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress.ToString();
        var endpoint = context.Request.Path;

        var redisKey = $"{ipAddress}:{endpoint}";

        var db = _redis.GetDatabase();
        var currentRequests = db.StringIncrement(redisKey);

        if (currentRequests == 1)
        {
            db.KeyExpire(redisKey, TimeSpan.FromMinutes(1));
        }

        if (currentRequests > 10)
        {
            context.Response.StatusCode = 429; // Too Many Requests
            await context.Response.WriteAsync("Too Many Requests");
            return;
        }

        await _next(context);
    }
}
