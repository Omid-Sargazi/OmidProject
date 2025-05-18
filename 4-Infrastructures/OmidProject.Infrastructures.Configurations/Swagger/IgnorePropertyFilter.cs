using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OmidProject.Infrastructures.Configurations.Swagger;

public class IgnorePropertyFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var ignoredProperties = context.MethodInfo.GetParameters()
            .SelectMany(p => p.ParameterType.GetProperties()
                .Where(prop => prop.GetCustomAttribute<Newtonsoft.Json.JsonIgnoreAttribute>() != null))
            .ToList();

        if (!ignoredProperties.Any())
        {
            return;
        }

        foreach (var property in ignoredProperties)
        {
            operation.Parameters = operation.Parameters
                .Where(p => (!p.Name.Contains(property.Name, StringComparison.InvariantCulture)))
                .ToList();
        }
    }
}