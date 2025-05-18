using System;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Host;
using OmidProject.Infrastructures.CommandDb;
using OmidProject.IntegrationTests.Configuration.Helpers;
using OmidProject.IntegrationTests.Services.Implementations;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace OmidProject.IntegrationTests.Configuration;

public class BaseTestClass : IClassFixture<WebApplicationFactory<Startup>>
{
    public readonly WebApplicationFactory<Startup> _factory;
    private readonly IServiceScope _scope;
    public string Name = "Authorization";
    public string Value = "";

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseTestClass" /> class.
    /// </summary>
    /// <param name="factory">The web application factory used for integration testing.</param>
    public BaseTestClass(WebApplicationFactory<Startup> factory)
    {
        _factory = ConfigureInMemoryDatabaseFactory(factory);
        _scope = _factory.Services.CreateScope();
    }

    protected IServiceProvider ServiceProvider => _scope.ServiceProvider;

    /// <summary>
    ///     Configures the web application factory to use in-memory databases for testing.
    /// </summary>
    /// <param name="factory">The web application factory to configure.</param>
    /// <returns>The configured web application factory with in-memory databases.</returns>
    private static WebApplicationFactory<Startup> ConfigureInMemoryDatabaseFactory(
        WebApplicationFactory<Startup> factory)
    {
        return factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Configure DbContext for the primary in-memory database.
                ConfigureInMemoryDatabase<OmidProjectCommandDb>(services, "TestDb");

                // Configure DbContext for the base in-memory database.
                ConfigureInMemoryDatabase<OmidProjectBaseCommandDb>(services, "TestBaseDb");

                // Configure ITestService for the base in Dependency Injection
                AddTestServices(services, typeof(ITestService));
            });
        });
    }

    /// <summary>
    ///     Helper method to add an in-memory database configuration for a given DbContext.
    /// </summary>
    /// <typeparam name="TContext">The type of the DbContext to configure.</typeparam>
    /// <param name="services">The service collection to which the DbContext will be added.</param>
    /// <param name="databaseName">The name of the in-memory database.</param>
    private static void ConfigureInMemoryDatabase<TContext>(IServiceCollection services, string databaseName)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
        {
            options.UseInMemoryDatabase(databaseName)
                .AddInterceptors(services.BuildServiceProvider()
                    .GetRequiredService<OmidProjectDbContextSaveChangesInterceptor>());
        });
    }

    /// <summary>
    ///     Adds test services to the specified service collection using the assembly reference
    ///     of the given handler interface type.
    /// </summary>
    /// <param name="serviceCollection">The service collection to which the services are added.</param>
    /// <param name="handlerInterfaceType">The type of the handler interface used for assembly reference.</param>
    private static void AddTestServices(IServiceCollection serviceCollection, Type handlerInterfaceType)
    {
        // Add scoped interfaces with assembly reference
        ConfigurationHelper.AddScopedInterfacesWithAssemblyReference(
            typeof(AuthenticationControllerService).Assembly,
            serviceCollection,
            handlerInterfaceType);
    }
}