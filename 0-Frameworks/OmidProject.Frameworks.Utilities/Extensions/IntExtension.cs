using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Transactions;

namespace OmidProject.Frameworks.Utilities.Extensions;

public static class IntExtension
{
    public static bool IsValidId(this int value)
    {
        return value > 0;
    }

    public static bool IsValidId(this int? value)
    {
        return value.HasValue && value.Value.IsValidId();
    }

    public static bool IsInvalidId(this int value)
    {
        return !IsValidId(value);
    }

    public static bool IsInvalidId(this int? value)
    {
        return !IsValidId(value);
    }

    public static bool IsDigit(this string value)
    {
        var regex = new Regex(@"^[ 0-9]*$");
        return regex.IsMatch(value);
    }

    public static bool IsValidNumber(this long? number)
    {
        return number.HasValue;
    }

    public static bool IsValidNumber(this int? number)
    {
        return number.HasValue;
    }

    public static double Pow(this int input, int pow)
    {
        return Math.Pow(input, pow);
    }
}