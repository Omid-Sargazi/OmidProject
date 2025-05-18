using OmidProject.Frameworks.Contracts.Common.Enums;

namespace OmidProject.Frameworks.Utilities.Extensions;

public static class ExtensionPersonType
{
    public static bool IsYear(this DateTimeInterval intervalType)
    {
        var result = intervalType is DateTimeInterval.Year;
        return result;
    }

    public static bool IsMonth(this DateTimeInterval intervalType)
    {
        var result = intervalType is DateTimeInterval.Month;
        return result;
    }

    public static bool IsDay(this DateTimeInterval intervalType)
    {
        var result = intervalType is DateTimeInterval.Day;
        return result;
    }

    public static bool IsHour(this DateTimeInterval intervalType)
    {
        var result = intervalType is DateTimeInterval.Hour;
        return result;
    }

    public static bool IsMinute(this DateTimeInterval intervalType)
    {
        var result = intervalType is DateTimeInterval.Minute;
        return result;
    }

    public static bool IsSecond(this DateTimeInterval intervalType)
    {
        var result = intervalType is DateTimeInterval.Second;
        return result;
    }

    public static bool IsMillisecond(this DateTimeInterval intervalType)
    {
        var result = intervalType is DateTimeInterval.Millisecond;
        return result;
    }
}