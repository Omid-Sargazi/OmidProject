using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace OmidProject.Frameworks.Utilities.Extensions;

public static class StringExtension
{
    public static bool IsEnglishString(this string value)
    {
        var result = false;

        var regex = new Regex(@"^[a-zA-Z0-9. -_?]*$");

        if (regex.IsMatch(value)) result = true;

        return result;
    }

    public static bool HasDirtyPersianString(this string value)
    {
        var result = false;
        var dirties = new[]
        {
            'ﮎ', 'ﮏ', 'ﮐ', 'ﮑ', 'ك', 'ي', ' ', '‌', 'ھ',
            '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹', //Persian
            '٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩', //Arabic
            '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹' //Urdu
        };
        result = value.Any(x => dirties.Contains(x));
        return result;
    }

    public static bool IsPersianString(this string value)
    {
        var result = false;
        var persianChars = " آابپتثجچحخدذرزژسشصضطظعغفقکگل‌منوهیكيئ۰۱۲۳۴۵۶۷۸۹";

        var regex = new Regex($"^[^{persianChars}]*[{persianChars}]{{2,}}[^{persianChars}]*$");

        if (regex.IsMatch(value.Trim().Trim('‌'))) result = true;

        return result;
    }

    public static bool IsPersianStringWithEnglishNumbers(this string value)
    {
        var result = false;

        var regex = new Regex(@"^[ ،|آأاِائبپتثجچحخدذرزژسشصضطظعغفقکگل‌منوؤهیئء0ك123456789۰۱۲۳۴۵۶۷۸۹()ي-]{2,}$");

        if (regex.IsMatch(value.Trim().Trim('‌'))) result = true;

        return result;
    }


    public static string FixPersianChars(this string str)
    {
        if (str.IsNullOrEmpty())
            return str;
        return str.Replace("ﮎ", "ک")
            .Replace("ﮏ", "ک")
            .Replace("ﮐ", "ک")
            .Replace("ﮑ", "ک")
            .Replace("ك", "ک")
            .Replace("ي", "ی")
            .Replace("ئ", "ی")
            .Replace("ى", "ی")
            .Replace(" ", " ")
            .Replace("‌", " ")
            .Replace("ٔ", "")
            .Replace("ھ", "ه")
            .Replace("دِ", "د")
            .Replace("بِ", "ب")
            .Replace("زِ", "ز")
            .Replace("شِ", "ش")
            .Replace("سِ", "س");
    }

    public static string FixPersianNumbersToEnglish(this string str)
    {
        if (str.IsNullOrEmpty())
            return str;
        return str
            //Persian
            .Replace("۰", "0")
            .Replace("۱", "1")
            .Replace("۲", "2")
            .Replace("۳", "3")
            .Replace("۴", "4")
            .Replace("۵", "5")
            .Replace("۶", "6")
            .Replace("۷", "7")
            .Replace("۸", "8")
            .Replace("۹", "9")
            //.Replace("/", ".")
            //Arabic
            .Replace("٠", "0")
            .Replace("١", "1")
            .Replace("٢", "2")
            .Replace("٣", "3")
            .Replace("٤", "4")
            .Replace("٥", "5")
            .Replace("٦", "6")
            .Replace("٧", "7")
            .Replace("٨", "8")
            .Replace("٩", "9");
    }

    public static string FixPersianCharsAndNumbers(this string str)
    {
        var result = str.FixPersianChars().FixPersianNumbersToEnglish();
        return result;
    }

    public static string ToPersianNumbers(this string str)
    {
        if (str.IsNullOrEmpty())
            return str;

        return str
            //Persian
            .Replace("0", "۰")
            .Replace("1", "۱")
            .Replace("2", "۲")
            .Replace("3", "۳")
            .Replace("4", "۴")
            .Replace("5", "۵")
            .Replace("6", "۶")
            .Replace("7", "۷")
            .Replace("8", "۸")
            .Replace("9", "۹");
    }

    public static string TrimToPersian(this string value)
    {
        return value.Trim();
    }

    public static bool IsNullOrEmpty(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool IsValidPostalCode(this string postalCode)
    {
        return !string.IsNullOrEmpty(postalCode) && postalCode.Length == 10;
    }

    public static bool IsInvalidPostalCode(this string postalCode)
    {
        return string.IsNullOrEmpty(postalCode) && postalCode.Length == 10;
    }

    public static string TrimForBackslash(this string value)
    {
        var result = "Has Error";

        var contentOne = value[0] == '[' ? value.Remove(0, 1) : value;

        var contentTwo = value[contentOne.Length - 1] == ']' ? contentOne.Remove(contentOne.Length - 1, 1) : value;
        ;

        var contentThree = contentTwo.Replace('\"', '"');

        result = new StringBuilder(contentThree).Replace(@"\", string.Empty).ToString();

        return result;
    }

    public static bool IsValidBase64(this string value)
    {
        value = value.Trim();
        return value.Length % 4 == 0 && Regex.IsMatch(value, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
    }

    public static string CleanBase64(this string value)
    {
        var result = value;

        if (!value.IsNullOrEmpty())
        {
            var spl = value.Split('/')[1];
            var format = spl.Split(';')[0];
            result = value.Replace($"data:image/{format};base64,", string.Empty);
        }

        return result;
    }

    public static bool IsValidToken(this string value)
    {
        value = value.Trim();
        if ((value.Length > 100) | (value.Length < 30)) return false;

        return Regex.IsMatch(value, @"^[a-zA-Z0-9-]+$", RegexOptions.None);
    }

    /// <summary>
    ///     This method removing [http://] [https://] [www.]
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetCleanDomainAddress(this string value)
    {
        value = value.ToLower()
            .Replace("http://", "")
            .Replace("https://", "")
            .Replace("www.", "");
        return value;
    }

    public static int ToInt(this string value)
    {
        var result = Convert.ToInt32(value);
        return result;
    }

    /// <summary>
    ///     Acceptable inputs:
    ///     +989122256442, +9809122256442, 989122256442, 9809122256442
    ///     00989122256442, 009809122256442, 09122256442, 9122256442
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string FixMobileNoFormat(this string number)
    {
        var rx = new Regex(@"((\+|00){0,1}98){0,1}0{0,1}(\d{10})");
        var result = rx.Replace(number, "+98$3");
        return result;
    }

    /// <summary>
    ///     Acceptable inputs:
    ///     +989122256442, +9809122256442, 989122256442, 9809122256442
    ///     00989122256442, 009809122256442, 09122256442, 9122256442
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsMobile(this string number)
    {
        var rx = new Regex(@"^((\+|00){0,1}98){0,1}0{0,1}(9\d{9})$");

        return rx.IsMatch(number.Trim());
    }

    public static bool IsTelephone(this string number)
    {
        //var notStartedWithTwoZeros = number.Length > 1 && number.Substring(0, 2) != "00";
        //var rx1 = new Regex(@"^(0{1}[1-9]){0,1}\d{4,11}$");
        //var rx2 = new Regex(@"^0[1-9]{1,2}0+$");
        var rx1 = new Regex(@"(?!^00\d+$)(?=^\d{4,11}$)(?!^0\d{2}0+$)(?!^0\d{2,5}$)");
        //return rx1.IsMatch(number) && !rx2.IsMatch(number) && notStartedWithTwoZeros;
        return rx1.IsMatch(number);
    }

    public static T FixNulls<T>(this T model) where T : class
    {
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
            if (property.GetValue(model) is null)
            {
                if (property.PropertyType.Name == typeof(string).Name)
                    property.SetValue(model, "");
                else if (property.PropertyType.Name == typeof(int).Name) property.SetValue(model, 0);
            }

        return model;
    }

    public static bool IsValidDateTime(this string date)
    {
        return DateTime.TryParse(date, out _);
    }

    public static bool IsShamsiDate(this string input)
    {
        if (input.IsNullOrEmpty()) return false;

        if (!Regex.IsMatch(input, @"^\d{4}([-/ ])\d{2}\1\d{2}$")) // بررسی صحت فرمت ورودی
        {
            if (Regex.IsMatch(input, @"^\d{4}([-/ ])\d{1,2}\1\d{1,2}$"))
            {
                var parts = input.Split(new[] { "-", "/", " " }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 3)
                {
                    var year = int.Parse(parts[0]);
                    var month = int.Parse(parts[1]);
                    var day = int.Parse(parts[2]);

                    if (month > 12 && day <= 12)
                    {
                        var temp = month;
                        month = day;
                        day = temp;
                    }

                    if (year < 1 || year > 9378 || month < 1 || month > 12 || day < 1 ||
                        day > new PersianCalendar().GetDaysInMonth(year, month)) return false;

                    return true;
                }

                return false;
            }

            return false;
        }

        {
            var persianCalendar = new PersianCalendar();

            var parts = input.Split('/', '-');
            var year = int.Parse(parts[0]);
            var month = int.Parse(parts[1]);
            var day = int.Parse(parts[2]);

            if (month > 12 && day <= 12)
            {
                var temp = month;
                month = day;
                day = temp;
            }

            if (year < 1 || year > 9378 || month < 1 || month > 12 || day < 1 ||
                day > persianCalendar.GetDaysInMonth(year, month)) return false;

            return true;
        }
    }

    public static string GetPersianYear(this string persianDate)
    {
        var result = "";
        try
        {
            result = persianDate[..4];
        }
        catch (Exception)
        {
            // ignored
        }

        return result;
    }

    public static string GetPersianMonth(this string persianDate)
    {
        var result = "";
        try
        {
            if (persianDate.Contains("/"))
                result = persianDate.Substring(5, 2);
            else result = persianDate.Substring(4, 2);
        }
        catch (Exception)
        {
            // ignored
        }

        return result;
    }

    public static string ConvertToLower(this string input)
    {
        var result = input.ToLower();
        return result;
    }

    public static string CleanHtml(this string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        foreach (var node in doc.DocumentNode.DescendantsAndSelf())
            if (!node.HasChildNodes && !string.IsNullOrEmpty(node.InnerHtml))
                node.InnerHtml = node.InnerHtml.Trim();

        return doc.DocumentNode.OuterHtml;
    }

    public static bool IsOnlyPersianLetters(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        // استفاده از عبارت منظم برای بررسی که آیا رشته شامل حروف الفبای فارسی است
        const string pattern = @"^[\u0600-\u06FF]+$";

        return Regex.IsMatch(input, pattern);
    }

    public static bool IsOnlyEnglishLetters(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        // استفاده از عبارت منظم برای بررسی که آیا رشته شامل حروف الفبای انگلیسی است
        const string pattern = @"^[A-Za-z]+$";

        return Regex.IsMatch(input, pattern);
    }

    public static bool IsValidPhoneNumber(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        // تبدیل به عدد
        if (!long.TryParse(input, out var number) || number <= 0)
            return false;

        // چک کردن طول
        if (input.Length != 11)
            return false;

        // چک کردن صفر شروع شده باشد
        // چک کردن بزرگتر از صفر باشد
        return input[0] == '0';
    }

    // تبدیل آرایه به رشته
    public static string ArrayToString<T>(this IEnumerable<T> input, char separator)
    {
        // جوین بر اساس جداکننده
        var result = string.Join(separator, input);

        return result;
    }

    // دریافت پارامز و برگشت کنار هم
    public static string ParamsToString(params string[] inputs)
    {
        // ساخت StringBuilder
        var builder = new StringBuilder();

        // حلقه زدن برای اضافه کردن پارام ها به بیلدر
        foreach (var input in inputs) builder.Append(input);

        return builder.ToString();
    }
}