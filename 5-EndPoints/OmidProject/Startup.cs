using System;
using System.Reflection;
using OmidProject.Applications.Application.MessageCodes.IdentityPersianErrorsHandler;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Host.Middleware;
using OmidProject.Infrastructures.CommandDb;
using OmidProject.Infrastructures.Configurations.DefaultData;
using OmidProject.Infrastructures.Configurations.RegisterTypes;
using OmidProject.Infrastructures.Configurations.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace OmidProject.Host;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var defaultDataConfig = Configuration.GetSection("defaultData");
        services.AddHealthChecks();

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<OmidProjectCommandDb>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<PersianIdentityErrorDescriber>()
            .AddRoles<ApplicationRole>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "OmidProject.Host", Version = "v1" });
        });

        //services.Configure<DefaultDataSchemas>(defaultDataConfig);

        services.AddConfigureAllServices(Configuration);
        services.AddOptions();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    //you can configure your custom policy
                    builder.WithOrigins("*").AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });


        #region پیکربندی Serilog

        Log.Logger = new LoggerConfiguration()
            //.WriteTo.Console
            .WriteTo.Seq("http://localhost:5341")
            //.ReadFrom.Configuration(defaultSeriLogConfig)
            .Filter.ByIncludingOnly(IsUserLog)
            .CreateLogger();

        services.AddLogging(builder =>
        {
            // اضافه کردن Serilog به لاگ‌گیری
            builder.AddSerilog();
        });

        #endregion
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            app.UseSwagger();

            //app.ConfigureSystemMessages(serviceProvider, assemblies);

            using (serviceProvider.CreateScope())
            {
                app.InitializerExecuteAsync(serviceProvider, Assembly.GetEntryAssembly())
                    .GetAwaiter()
                    .GetResult();
            }

            app.SwaggerConfigure();

            //var detectionAccessibleForms = serviceProvider.GetRequiredService(typeof(DetectionAccessibleForms)) as DetectionAccessibleForms;

            //detectionAccessibleForms.StartExploring();
        }

        app.UseCors(
            options => options.WithOrigins("*").AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
        );

        app.UseMiddleware<CustomSerilogMiddleware>();

        app.UseMiddleware<CustomExceptionMiddleware>();

        app.UseHsts();

        //app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/healthcheck");
            endpoints.MapControllers();
        });
    }

    private static bool IsUserLog(LogEvent evt)
    {
        if (evt.Properties.TryGetValue("IsCustomLog", out var value) && value is ScalarValue scalarValue)
            return scalarValue.Value is bool and true;

        return false;
    }
}