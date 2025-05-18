using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace OmidProject.Infrastructures.Configurations.DefaultData;

public static class ConfigureInitializer
{
    public static async Task InitializerExecuteAsync(this IApplicationBuilder app, IServiceProvider? serviceProvider,
        Assembly assembly)
    {
        if (serviceProvider != null)
        {
            var initializer = serviceProvider.GetRequiredService<DefaultDataInitializer>();
            var loader = serviceProvider.GetRequiredService<InitialDataLoader>();

            try
            {
                await initializer.Execute(assembly);
                await loader.Execute();
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}