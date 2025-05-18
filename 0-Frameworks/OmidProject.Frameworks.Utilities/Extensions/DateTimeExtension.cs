using System.Globalization;
using OmidProject.Frameworks.Contracts.Common.Enums;

namespace OmidProject.Frameworks.Utilities.Extensions;

public static class DateTimeExtension
{
    public static bool IsGreaterThan(this DateTime value, DateTime dateTime)
    {
        return value.Date > dateTime.Date;
    }

    public static bool IsGreaterOrEqualThan(this DateTime value, DateTime dateTime)
    {
        return value.Date >= dateTime.Date;
    }

    public static bool IsGreaterThanNow(this DateTime value)
    {
        return value.Date > DateTime.Now;
    }

    public static bool IsGreaterOrEqualThanNow(this DateTime value)
    {
        return value.Date >= DateTime.Now;
    }

    public static bool IsLessThan(this DateTime value, DateTime dateTime)
    {
        return value.Date < dateTime.Date;
    }

    public static bool IsLessThenOrEqualTo(this DateTime value, DateTime dateTime)
    {
        return value.Date <= dateTime.Date;
    }

    public static bool IsValid(this DateTime? date)
    {
        return date != null && date.HasValue && date.Value > DateTime.MinValue;
    }

    public static DateTime ConvertToShamsi(string input)
    {
        var persianCalendar = new PersianCalendar();

        var parts = input.Split('/', '-', ' ');

        var year = int.Parse(parts[0]);
        var month = int.Parse(parts[1]);
        var day = int.Parse(parts[2]);

        if (month > 12 && day <= 12)
        {
            var temp = month;
            month = day;
            day = temp;
        }

        return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
    }

    public static bool IsExpired(this DateTime date)
    {
        return date < DateTime.Now;
    }

    public static bool IsNotExpired(this DateTime date)
    {
        return !IsExpired(date);
    }

    public static bool IsNotExpired(this DateTime date, double addInterval, DateTimeInterval intervalType)
    {
        return !IsExpired(date, addInterval, intervalType);
    }

    public static bool IsExpired(this DateTime date, double addInterval, DateTimeInterval intervalType)
    {
        return intervalType switch
        {
            DateTimeInterval.Year => date.AddYears((int)addInterval).IsExpired(),
            DateTimeInterval.Month => date.AddMonths((int)addInterval).IsExpired(),
            DateTimeInterval.Day => date.AddDays(addInterval).IsExpired(),
            DateTimeInterval.Hour => date.AddHours(addInterval).IsExpired(),
            DateTimeInterval.Minute => date.AddMinutes(addInterval).IsExpired(),
            DateTimeInterval.Second => date.AddSeconds(addInterval).IsExpired(),
            _ => date.AddMilliseconds(addInterval).IsExpired()
        };
    }
}