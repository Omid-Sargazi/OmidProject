using OmidProject.Frameworks.Contracts.Common;
using Newtonsoft.Json;

namespace OmidProject.Frameworks.Utilities.Extensions;

public static class ObjectExtension
{
    public static bool ObjectIsAllNullOrEmpty(this object myObject)
    {
        if (myObject == null)
            return true;

        foreach (var pi in myObject.GetType().GetProperties())
            if (pi.PropertyType == typeof(string))
            {
                var value = (string) pi.GetValue(myObject);
                if (!string.IsNullOrEmpty(value)) return false;
            }

        return true;
    }

    public static bool ObjectIsAnyNullOrEmpty(this object myObject)
    {
        if (myObject == null)
            return true;

        foreach (var pi in myObject.GetType().GetProperties())
            if (pi.PropertyType == typeof(string))
            {
                var value = (string) pi.GetValue(myObject);
                if (string.IsNullOrEmpty(value)) return false;
            }

        return true;
    }

    public static T MapTo<T>(this object source)
    {
        if (source == null)
            return (T) Activator.CreateInstance(typeof(T));

        var targetType = typeof(T);
        var target = (T) Activator.CreateInstance(targetType);

        var sourceProperties = source.GetType().GetProperties();
        var targetProperties = targetType.GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            var targetProperty = targetProperties.FirstOrDefault(p =>
                p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);

            if (targetProperty != null && targetProperty.CanWrite)
            {
                var sourceValue = sourceProperty.GetValue(source);
                var targetValueType = targetProperty.PropertyType;

                // اگر نوع مقصد nullable است
                if (Nullable.GetUnderlyingType(targetValueType) != null)
                {
                    // چک کنید که مقدار منبع null نباشد
                    if (sourceValue != null) targetProperty.SetValue(target, sourceValue);
                }
                else
                {
                    // اگر نوع مقصد nullable نیست، مستقیماً مقدار را جابه‌جا کنید
                    targetProperty.SetValue(target, sourceValue);
                }
            }
        }

        return target;
    }

    public static void FixAllStringPropertiesObject(this object? obj)
    {
        if (obj == null)
            return;

        var properties = obj.GetType().GetProperties();
        foreach (var property in properties)
            if (property.PropertyType == typeof(string))
            {
                var value = (string) property.GetValue(obj)!;
                if (!value.IsNullOrEmpty())
                    // اعمال تابع خاص به رشته
                    value.FixPersianChars();
            }
            else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
            {
                // اگر فیلد یک شیء است ولی نوع آن رشته نیست، متد را بر روی شیء دیگر فراخوانی می‌کنیم
                FixAllStringPropertiesObject(property.GetValue(obj));
            }
    }

    public static async Task<T> DeserializeResponseAsync<T>(this HttpResponseMessage response)
    {
        var responseString = await response.Content.ReadAsStringAsync();
        var apiResult = JsonConvert.DeserializeObject<ApiResult>(responseString);
        return JsonConvert.DeserializeObject<T>(apiResult.Result.ToString());
    }
}