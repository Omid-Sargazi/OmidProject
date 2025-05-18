using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace OmidProject.Frameworks.Contracts.Extensions;

public static class ConfigureReflectionExtension
{
    private static bool IsSameInterface(this Type type, Type handlerInterface)
    {
        var list = type.GetInterfaces();

        var result = list.Any(i => i.Name == handlerInterface.Name);

        return result;
    }

    private static Type GetSameInterface(Type[] types)
    {
        var directInterfaces = types.Except
            (types.SelectMany(t => t.GetInterfaces())).ToList();

        var result = directInterfaces.First();

        return result;
    }

    private static Type GetSameInterface(Type[] types, Type handlerInterface)
    {
        var result = types.First();

        return result;
    }

    public static void AddScopedInterfacesWithAssemblyReference(Assembly assembly, IServiceCollection serviceCollection,
        Type handlerInterface)
    {
        var implementations = assembly.GetTypes().Where(t => t.IsSameInterface(handlerInterface) && !t.IsInterface);

        implementations = implementations.Where(x => !x.IsGenericType);

        foreach (var implementation in implementations)
        {
            var serviceType = GetSameInterface(implementation.GetInterfaces());

            serviceCollection.AddScoped(serviceType, implementation);
        }
    }
    public static void AddTransientInterfacesWithAssemblyReference(Assembly assembly, IServiceCollection serviceCollection,
        Type handlerInterface)
    {
        var implementations = assembly.GetTypes().Where(t => t.IsSameInterface(handlerInterface) && !t.IsInterface);

        implementations = implementations.Where(x => !x.IsGenericType);

        foreach (var implementation in implementations)
        {
            var serviceType = GetSameInterface(implementation.GetInterfaces());

            serviceCollection.AddTransient(serviceType, implementation);
        }
    }

    public static void AddSingletonInterfacesWithAssemblyReference(Assembly assembly,
        IServiceCollection serviceCollection, Type handlerInterface)
    {
        var implementations = assembly.GetTypes().Where(t => t.IsSameInterface(handlerInterface) && !t.IsInterface);

        foreach (var implementation in implementations)
        {
            var serviceType = GetSameInterface(implementation.GetInterfaces(), handlerInterface);

            serviceCollection.AddSingleton(serviceType, implementation);
        }
    }
}