using Serilog;
using Serilog.Events;
using TatterFitness.Api.Extensions;

WebApplicationBuilder webAppBuilder = WebApplication.CreateBuilder(args);
IServiceCollection services = webAppBuilder.Services;
ConfigurationManager configuration = webAppBuilder.Configuration;


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .CreateLogger();

webAppBuilder.Host.UseSerilog();
services
    .AddTatterFitnessCors(configuration)
    .AddTatterFitnessSwagger(webAppBuilder)
    .RegisterTatterFitnessDi(configuration)
    .AddEndpointsApiExplorer()
    .AddControllers()
    .AddNewtonsoftJson();

var app = webAppBuilder.Build();
app.UseSwagger()
    .UseSwaggerUI()
    .UseAuthorization();

app.MapControllers();
app.Run();

