using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using TatterFitness.Bll.Interfaces.Services;
using TatterFitness.Bll.Services;
using TatterFitness.Dal.Interfaces.Persistance;
using TatterFitness.Dal.Persistence;
using TatterFitness.Bll.Mapping;
using Serilog;

namespace TatterFitness.Api.Extensions
{
    public static class ServicesExtensions
    {
        static public IServiceCollection AddTatterFitnessCors(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsOrigins",
                                  builder =>
                                  {
                                      builder.WithMethods("GET", "DELETE", "POST", "PATCH");
                                      builder.AllowAnyOrigin();
                                      builder.WithHeaders(HeaderNames.ContentType);
                                  });
            });

            return services;
        }

        static public IServiceCollection AddTatterFitnessSwagger(this IServiceCollection services, WebApplicationBuilder webAppBuilder)
        {
            services.AddSwaggerGen(swagger =>
            {
                var title = "TatterFitAPI";
                if (webAppBuilder.Environment.IsDevelopment())
                {
                    title = $"DEBUG-{title}";
                }
                else
                {
                    title = $"RELEASE-{title}";
                }

                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = title });
                swagger.EnableAnnotations();

                var filePath = Path.Combine(AppContext.BaseDirectory, "TatterFitness.Api.xml");
                swagger.IncludeXmlComments(filePath);

            });

            return services;
        }

        static public IServiceCollection RegisterTatterFitnessDi(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAutoMapper(typeof(ModelMapping));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IExercisesService, ExercisesService>();
            services.AddScoped<IWorkoutsService, WorkoutsService>();
            services.AddScoped<IHistoriesService, HistoriesService>();
            services.AddScoped<IRoutinesService, RoutinesService>();
            services.AddScoped<IExerciseModifiersService, ExerciseModifiersService>();
            services.AddScoped<IRoutineExercisesService, RoutineExercisesService>();
            services.AddScoped<IWorkoutExerciseModifiersService, WorkoutExerciseModifiersService>();
            services.AddScoped<IWorkoutExercisesService, WorkoutExercisesService>();
            services.AddScoped<IWorkoutExerciseSetsService, WorkoutExerciseSetsService>();
            services.AddScoped<IVideoService, VideoService>();

            services.AddDbContext<TatterDb>(options =>
            {
                //options.EnableSensitiveDataLogging();
                //options.LogTo(Log.Logger.Information, LogLevel.Information);
                options.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
                options.UseSqlServer(
                    configuration.GetConnectionString("TatterFitnessDb"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });

            return services;
        }
    }
}
