public class RequestTimerMiddleware(RequestDelegate next, ILogger<RequestTimerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var start = DateTimeOffset.UtcNow;
        await next(context);
        logger.LogInformation($"Request time: {(DateTimeOffset.UtcNow - start).Milliseconds}ms");
    }
}