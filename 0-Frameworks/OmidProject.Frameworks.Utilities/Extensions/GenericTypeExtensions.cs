using System.ComponentModel;
using System.Reflection;

namespace OmidProject.Frameworks.Utilities.Extensions;

public static class GenericTypeExtensions
{
    public static List<PropertyInfo> GetPropertiesWithDisplayName<T>()
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        return properties.Where(p => p.GetCustomAttribute<DisplayNameAttribute>() != null).ToList();
    }

    // دریافت نام کامل از یک شئ
    public static string GetFullName<T>(this T obj)
    {
        var firstNameProperty = typeof(T).GetProperty("FirstName", BindingFlags.Public | BindingFlags.Instance);
        var lastNameProperty = typeof(T).GetProperty("LastName", BindingFlags.Public | BindingFlags.Instance);
        if (firstNameProperty is null || lastNameProperty is null) return string.Empty;
        var firstName = firstNameProperty.GetValue(obj, null);
        var lastName = lastNameProperty.GetValue(obj, null);
        return $"{firstName} {lastName}";
    }
}
