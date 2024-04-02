using System;
using System.Reflection;
using Api.EF;
using Api.EF.SQL;
using Api.Middlewares;
using Api.Repositories;
using Api.Services;
using Api.Services.StackExchange.TagsService;
using Api.Services.StackExchangeAuthService;
using Api.Services.StackExchangeAuthService.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Api
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHostedService<AppInitializer>();
            services.AddInitializer<DataInitializer>();
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ITagsService, TagsService>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IStackExchangeAuthService, StackExchangeAuthService>();
            services.AddScoped<IConfigurationStackExchange, ConfigurationStackExchange>();
            services.AddScoped<ErrorHandlerMiddleware>();
            services.AddSql<DatabaseContext>();
            return services;
        }

        public static IApplicationBuilder UseServices(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            return app;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName)
            where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName)
            where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            return configuration.GetOptions<T>(sectionName);
        }

        public static IServiceCollection AddInitializer<T>(this IServiceCollection services)
            where T : class, IInitializer => services.AddTransient<IInitializer, T>();
    }
}
