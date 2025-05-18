using System.Text;
using OmidProject.Applications.Application.AccessibleFormHandlers.CommandHandlers;
using OmidProject.Applications.Application.Service;
using OmidProject.Applications.Contracts.AccessibleFormContracts.Validator;
using OmidProject.Applications.Contracts.Repository;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Extensions;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Infrastructures.ACL.Sms;
using OmidProject.Infrastructures.CommandDb;
using OmidProject.Infrastructures.CommandDb.Repository;
using OmidProject.Infrastructures.Configurations.DefaultData;
using OmidProject.Infrastructures.Configurations.Swagger;
using OmidProject.JobService;
using OmidProject.Messaging.Configurations;
using OmidProject.Messaging.Consumers.RabbitMQ.SendSms;
using OmidProject.Messaging.Producers.RabbitMQ.SendSms;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace OmidProject.Infrastructures.Configurations.RegisterTypes;

public static class ConfigureServicesExtension
{
    public static void AddCommandsDBContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<OmidProjectCommandDb>(
            (provider, options) =>
            {
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                //options.UseSqlServer(connectionString, builder =>
                //{
                //    builder.EnableRetryOnFailure(
                //        maxRetryCount: 5,
                //        maxRetryDelay: TimeSpan.FromSeconds(5),
                //        errorNumbersToAdd: null);
                ////});
                options.AddInterceptors(provider.GetRequiredService<OmidProjectDbContextSaveChangesInterceptor>());
            }
        );
    }

    public static void AddBaseCommandDBContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<OmidProjectBaseCommandDb>(
            (provider, options) =>
            {
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                //options.UseSqlServer(connectionString, builder =>
                //{
                //    builder.EnableRetryOnFailure(
                //        maxRetryCount: 5,
                //        maxRetryDelay: TimeSpan.FromSeconds(5),
                //        errorNumbersToAdd: null);
                ////});
                options.AddInterceptors(provider.GetRequiredService<OmidProjectDbContextSaveChangesInterceptor>());
            }
        );
    }

    public static void AddConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("OmidProject");
        var useSqlServer = configuration.GetSection("USE_SQL_SERVER");
        //services.AddSettings(configuration);
        services.AddScoped<OmidProjectDbContextSaveChangesInterceptor>();
        services.AddScoped<DefaultDataInitializer>();
        services.AddScoped<InitialDataLoader>();
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddSettings(configuration);
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        if (useSqlServer.Value == "True")
        {
            services.AddCommandsDBContext(conn);
            services.AddBaseCommandDBContext(conn);
        }
        services.AddWithContract();
        services.AddMemoryCache();
        services.AddValidatorsFromAssemblyContaining<AddAccessibleFormCommandValidator>();
        //services.AddAuthenticationSettings();
        services.AddCors();
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        #region Config RabbitMQ

        services.ConfigurationRabbitMqServices(configuration);
        services.ConfigurationRabbitMqJobServices();

        #endregion

        services.AddHostedService<CleanUpTestUserData>();
    }

    public static void AddConfigureAllServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddVersion();
        services.AddSwagger();

        services.AddControllers();

        var key = Encoding.ASCII.GetBytes(configuration.GetSection("JwtSetting:Secret").Value);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Events = new JwtBearerEvents
            {
                OnTokenValidated = context => { return Task.CompletedTask; }
            };
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        AddConfigureServices(services, configuration);
    }

    private static void AddVersion(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });
    }

    public static void AddWithContract(this IServiceCollection serviceCollection)
    {
        AddRepositories(serviceCollection, typeof(IRepository));
        AddACLServices(serviceCollection, typeof(IACLService));
        AddServices(serviceCollection, typeof(IService));
        AddIdentityUser(serviceCollection, typeof(ApplicationUser));
        AddCommandHandlers(serviceCollection, typeof(IQueryHandler<,>));
        AddCommandHandlers(serviceCollection, typeof(ICommandHandler<,>));
        AddRabbitMqConsumers(serviceCollection, typeof(IRabbitMq));
        AddRabbitMqProducers(serviceCollection, typeof(IRabbitMq));

        serviceCollection.AddScoped<IDistributor, Distributor>();
    }

    private static void AddCommandHandlers(IServiceCollection serviceCollection, Type handlerInterface)
    {
        ConfigureReflectionExtension.AddScopedInterfacesWithAssemblyReference(
            typeof(AddAccessibleFormCommandHandler).Assembly,
            serviceCollection, handlerInterface);
    }

    private static void AddRepositories(IServiceCollection serviceCollection, Type handlerInterface)
    {
        ConfigureReflectionExtension.AddScopedInterfacesWithAssemblyReference(typeof(AccessibleFormRepository).Assembly,
            serviceCollection, handlerInterface);
    }

    private static void AddACLServices(IServiceCollection serviceCollection, Type handlerInterface)
    {
        ConfigureReflectionExtension.AddScopedInterfacesWithAssemblyReference(typeof(SmsAclService).Assembly,
            serviceCollection, handlerInterface);
    }

    private static void AddServices(IServiceCollection serviceCollection, Type handlerInterface)
    {
        ConfigureReflectionExtension.AddScopedInterfacesWithAssemblyReference(typeof(ProjectService).Assembly,
            serviceCollection, handlerInterface);
    }

    private static void AddIdentityUser(IServiceCollection serviceCollection, Type handlerInterface)
    {
        ConfigureReflectionExtension.AddScopedInterfacesWithAssemblyReference(typeof(ApplicationUser).Assembly,
            serviceCollection, handlerInterface);
    }

    private static void AddRabbitMqConsumers(IServiceCollection serviceCollection, Type handlerInterface)
    {
        ConfigureReflectionExtension.AddTransientInterfacesWithAssemblyReference(typeof(RabbitMQConsumer).Assembly,
            serviceCollection, handlerInterface);
    }

    private static void AddRabbitMqProducers(IServiceCollection serviceCollection, Type handlerInterface)
    {
        ConfigureReflectionExtension.AddTransientInterfacesWithAssemblyReference(typeof(RabbitMQProducer).Assembly,
            serviceCollection, handlerInterface);
    }
}