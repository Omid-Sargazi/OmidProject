using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OmidProject.Frameworks.Utilities.Extensions;

public static class DateTimeConvertorExtension
{
    private static CultureInfo _culture;

    private static CultureInfo GetPersianCulture()
    {
        if (_culture == null)
        {
            _culture = new CultureInfo("fa-IR");
            var formatInfo = _culture.DateTimeFormat;
            formatInfo.AbbreviatedDayNames = new[] {"ی", "د", "س", "چ", "پ", "ج", "ش"};
            formatInfo.DayNames = new[] {"یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنجشنبه", "جمعه", "شنبه"};
            var monthNames = new[]
            {
                "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن",
                "اسفند",
                ""
            };
            formatInfo.AbbreviatedMonthNames =
                formatInfo.MonthNames =
                    formatInfo.MonthGenitiveNames =
                        formatInfo.AbbreviatedMonthGenitiveNames = monthNames;

            formatInfo.AMDesignator = "ق.ظ";
            formatInfo.PMDesignator = "ب.ظ";
            formatInfo.ShortDatePattern = "yyyy/MM/dd";
            formatInfo.LongDatePattern = "dddd, dd MMMM,yyyy";
            formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
            Calendar calendar = new PersianCalendar();

            var fieldInfo = _culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo != null) fieldInfo.SetValue(_culture, calendar);

            var info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
            if (info != null) info.SetValue(formatInfo, calendar);

            _culture.NumberFormat.NumberDecimalSeparator = "/";
            _culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
            _culture.NumberFormat.NumberNegativePattern = 0;
        }

        return _culture;
    }

    /// <summary>
    ///     The persian date value must have this format: 'yyyy/mm/dd hh:mm'
    /// </summary>
    /// <param name="persianDate"></param>
    /// <returns></returns>
    public static bool IsPersianDateTime(this string persianDate)
    {
        var year = @"(13\d{2}|14\d{2})";
        var dateSeperator = @"[/-]{0,1}";
        var month = @"(0[1-9]|1[012])";
        var day = @"(0[1-9]|[12][0-9]|3[01])";
        var time24h = @"([01][0-9]|2[0-3]):([0-5]?[0-9])";
        var amPM = @" (([aApP][mM])|([\u0642\u0628]\.[\u0638]))";
        var time12h = @"(0[1-9]|1[0-2]):([0-5]?[0-9])" + amPM;
        var time = @"( ((" + time12h + ")|(" + time24h + "))){0,1}";

        return Regex.IsMatch(persianDate, $"^{year}{dateSeperator}{month}{dateSeperator}{day}{time}$");
        //return Regex.IsMatch(persianDate, @"^(13\d{2}|14\d{2})[/-]{0,1}(0[1-9]|1[012])[/-]{0,1}(0[1-9]|[12][0-9]|3[01])(( (0[1-9]|1[0-2]):([0-5]?[0-9])( (([aApP][mM])|([\u0642\u0628]\.[\u0638]))){0,1})|( ([01][0-9]|2[0-3]):([0-5]?[0-9]))){0,1}$");
    }

    public static string ToPersianYear(this DateTime date, string format = "yyyy")
    {
        if (date == DateTime.MinValue) return null;

        return date.ToString(format, GetPersianCulture());
    }

    public static int ToPersianYearNumber(this DateTime date, string format = "yyyy")
    {
        var year = Convert.ToInt32(ToPersianYear(date, format));

        return year;
    }

    public static string ToPersianMonth(this DateTime date, string format = "MM")
    {
        if (date == DateTime.MinValue) return null;

        return date.ToString(format, GetPersianCulture());
    }

    public static int ToPersianMonthNumber(this DateTime date, string format = "MM")
    {
        var month = Convert.ToInt32(ToPersianMonth(date, format));

        return month;
    }

    public static string ToPersianDate(this DateTime date, string format = "yyyy/MM/dd")
    {
        if (date == DateTime.MinValue) return null;

        return date.ToString(format, GetPersianCulture());
    }

    public static string ToPersianDate(this DateTime? date, string format = "yyyy/MM/dd")
    {
        if (date == null) return null;

        return date.Value.ToString(format, GetPersianCulture());
    }

    public static string ToPersianDateTime(this DateTime? dateTime)
    {
        if (dateTime == null) return null;

        return ToPersianDate(dateTime.Value);
    }

    public static string ToPersianDateTime(this DateTime dateTime)
    {
        return ToPersianDate(dateTime);
    }

    public static string ToPersianDate(this DateTime dateTime)
    {
        if (dateTime == DateTime.MinValue) return null;

        var persianCalendar = new PersianCalendar();
        var year = persianCalendar.GetYear(dateTime).ToString();
        var month = persianCalendar.GetMonth(dateTime).ToString().PadLeft(2, '0');
        var day = persianCalendar.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0');
        var hour = dateTime.Hour.ToString().PadLeft(2, '0');
        var minute = dateTime.Minute.ToString().PadLeft(2, '0');
        var second = dateTime.Second.ToString().PadLeft(2, '0');
        return string.Format("{0}/{1}/{2} {3}:{4}:{5}", year, month, day, hour, minute, second);
    }


    public static string ToPersianDate(this string dateTime)
    {
        return ToPersianDate(DateTime.Parse(dateTime));
    }

    /// <summary>
    ///     The persian date value must have this format: 'yyyy/mm/dd hh:mm'
    /// </summary>
    /// <param name="persianDate"></param>
    /// <returns></returns>
    public static DateTime ToMiladiDate(this string persianDate)
    {
        if (string.IsNullOrEmpty(persianDate)
            || !persianDate.IsPersianDateTime())
            throw new ArgumentException($"{nameof(persianDate)}:'{persianDate}' argument is not valid.");

        if (persianDate.Length >= 10)
        {
            persianDate = persianDate.Substring(0, 10);
            persianDate = persianDate.Replace("/", "");
            persianDate = persianDate.Replace("-", "");
        }

        var persianCalendar = new PersianCalendar();
        var year = Convert.ToInt32(persianDate.Substring(0, 4));
        var month = Convert.ToInt32(persianDate.Substring(4, 2));
        var day = Convert.ToInt32(persianDate.Substring(6, 2));
        return new DateTime(year, month, day, persianCalendar);
    }

    public static string FormatBySlash(this string value)
    {
        var result = value;

        if (!value.IsNullOrEmpty() && value.Length == 8)
        {
            var year = value.Substring(0, 4);
            var month = value.Substring(4, 2);
            var day = value.Substring(6, 2);
            result = $"{year}/{month}/{day}";
        }

        return result;
    }
}