using System.Reflection;
using database.Config;
using Microsoft.OpenApi.Models;
using services.Config;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nome da Sua API", Version = "v1" });
});
builder.Services.AddControllers();


builder.Services.ConfigureContext(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.ConfigureRepositories();
builder.Services.AddHostedServices();

WebApplication app = builder.Build();

app.UseRouting();
app.MapControllers();

try
{
    await app.RunAsync();
}
catch (Exception exception)
{
    app.Logger.LogError(exception, "Stopped program because of exception");
}