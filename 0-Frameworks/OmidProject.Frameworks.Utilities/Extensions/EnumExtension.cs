using System.ComponentModel;
using OmidProject.Frameworks.Contracts.Common.Enums;

namespace OmidProject.Frameworks.Utilities.Extensions;

public static class EnumExtension
{
    public static string GetDescription(this Enum value)
    {
        var result = (DescriptionAttribute[])value
            .GetType()
            .GetField(value.ToString())?
            .GetCustomAttributes(typeof(DescriptionAttribute),
                false);
        return result is { Length: > 0 }
            ? GetSecondPartWithDashSeparator(result[0].Description)
            : "نامشخص";
    }

    private static string GetSecondPartWithDashSeparator(string description)
    {
        if (description.Contains('-')) return description.Split('-')[1].Trim();
        return description;
    }

    public static T ToEnum<T>(this int value)
    {
        return (T)Enum.Parse(typeof(T), value.ToString());
    }

    public static bool IsDefined(this Enum value)
    {
        return Enum.IsDefined(value.GetType(), value);
    }

    public static bool IsUndefined(this Enum value)
    {
        return !Enum.IsDefined(value.GetType(), value);
    }


    public static bool IsNullEnum(this Type t)
    {
        var u = Nullable.GetUnderlyingType(t);
        var a = ContentLanguage.English;
        return u != null && u.IsEnum;
    }

    // دریافت نام بر اساس عدد
    public static string? GetNameFromValue<TEnum>(this TEnum enumType, int value) where TEnum : Enum
    {
        return Enum.GetName(typeof(TEnum), value);
    }

    // دریافت عدد بر اساس نام
    public static int GetValueFromName<TEnum>(this TEnum enumType, string name) where TEnum : Enum
    {
        return (int)Enum.Parse(typeof(TEnum), name);
    }
}