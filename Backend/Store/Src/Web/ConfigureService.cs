using Domain.Exceptions;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.SeedData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Middleware;

namespace Web;

public static class ConfigureService
{
    public static IServiceCollection AddWebConfigureServices(this WebApplicationBuilder builder 
        , IConfiguration configuration)
    {
        builder.Services.AddControllers();
        ApiBehaviourOptions(builder);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //CORS 
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(configuration["CorsAddress:AddressHttp"] , configuration["CorsAddress:AddressHttps"]);
            });
        });
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddDistributedMemoryCache();
        return builder.Services;
    }

    private static void ApiBehaviourOptions(WebApplicationBuilder builder)
    {
        //TODO check this 
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(s => s.Value.Errors)
                    .Select(c => c.ErrorMessage)
                    .ToList();
                return new BadRequestObjectResult(new ApiToReturn(400, errors));
            };
        });
    }

    public static async Task<IApplicationBuilder> AddWebAppServices(this WebApplication app)
    {
        app.UseMiddleware<MiddlewareExceptionHandler>();
        app.UseStaticFiles();//access to wwwroot
        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var context = services.GetRequiredService<ApplicationDbContext>();

        #region auto migration

        try
        {
            await context.Database.MigrateAsync();
            await GenerateFakeData.SeedDataAsync(context, loggerFactory);
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(e, "migration error");
        }

        #endregion

        app.UseRouting();
        app.UseCors("CorsPolicy");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.MapControllers();
        await app.RunAsync();
        return app;
    }
}