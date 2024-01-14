// В проекте Basket.Host
using Basket.Host.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

// Регистрация Middleware для ограничения запросов
app.UseRateLimiter();


app.Use(async (context, next) =>
{

    var rateLimitAttribute = context.GetEndpoint()?.Metadata.GetMetadata<RateLimitAttribute>();

    if (rateLimitAttribute != null)
    {
      
        await context.RequestServices.GetRequiredService<RateLimiterMiddleware>().Invoke(context, next);
        return;
    }

    await next();
});

app.MapControllers();

app.Run();
