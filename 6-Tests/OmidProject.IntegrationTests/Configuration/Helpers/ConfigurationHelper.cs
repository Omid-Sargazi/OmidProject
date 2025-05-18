using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace OmidProject.IntegrationTests.Configuration.Helpers;

public static class ConfigurationHelper
{
    /// <summary>
    ///     Registers all non-generic implementations of the specified handler interface from the given assembly as scoped
    ///     services.
    /// </summary>
    /// <param name="assembly">The assembly to scan for implementations.</param>
    /// <param name="serviceCollection">The service collection to which the services should be added.</param>
    /// <param name="handlerInterface">The interface to be implemented by the services.</param>
    public static void AddScopedInterfacesWithAssemblyReference(Assembly assembly, IServiceCollection serviceCollection,
        Type handlerInterface)
    {
        var implementations = assembly.GetTypes()
            .Where(type =>
                type is { IsClass: true, IsGenericType: false } && ImplementsInterface(type, handlerInterface));

        foreach (var implementation in implementations)
        {
            var serviceType = GetImplementedInterface(implementation.GetInterfaces());
            serviceCollection.AddScoped(serviceType, implementation);
        }
    }

    /// <summary>
    ///     Checks if the specified type implements the given interface.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <param name="handlerInterface">The interface to check against.</param>
    /// <returns>True if the type implements the interface; otherwise, false.</returns>
    private static bool ImplementsInterface(Type type, Type handlerInterface)
    {
        return type.GetInterfaces().Any(face => face == handlerInterface);
    }

    /// <summary>
    ///     Gets the first directly implemented interface from the array of interfaces.
    /// </summary>
    /// <param name="interfaces">An array of interfaces implemented by the type.</param>
    /// <returns>The first directly implemented interface.</returns>
    private static Type GetImplementedInterface(Type[] interfaces)
    {
        // Direct interfaces are those that are not inherited from other interfaces.
        var directlyImplementedInterfaces =
            interfaces.Except(interfaces.SelectMany(t => t.GetInterfaces()).Distinct()).ToList();

        // Assuming there's always at least one directly implemented interface
        return directlyImplementedInterfaces.First();
    }
}