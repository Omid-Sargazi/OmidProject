using System.Net;
using System.Text.RegularExpressions;

namespace OmidProject.Frameworks.Utilities.Extensions;

public static class ValueObjectExtensions
{
    /// <summary>
    ///     Email Extensions
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>

    #region Email

    public static bool IsEmailValid(this string email)
    {
        return Regex.IsMatch(email,
            @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            RegexOptions.IgnoreCase);
    }

    #endregion

    /// <summary>
    ///     EnglishName Extension
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>

    #region EnglishName

    private static bool IsEnglishString(this string value)
    {
        var result = false;

        var regex = new Regex(@"^[ a-zA-Z0-9]*$");

        if (regex.IsMatch(value)) result = true;

        return result;
    }

    #endregion

    /// <summary>
    ///     PostalCode Extension
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>

    #region PostalCode

    public static bool IsPostalCodeValid(this string value)
    {
        var digits = value.Length;

        return digits == 10;
    }

    #endregion

    /// <summary>
    ///     User Extension
    /// </summary>
    /// <param name="nationalCode"></param>
    /// <returns></returns>

    #region User

    public static bool IsNationalCodeValid(this string nationalCode)
    {
        if (nationalCode.Length != 10) return false;

        var allDigitEqual = new[]
        {
            "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666",
            "7777777777", "8888888888", "9999999999"
        };
        if (allDigitEqual.Contains(nationalCode)) return false;

        var chArray = nationalCode.ToCharArray();
        var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
        var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
        var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
        var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
        var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
        var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
        var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
        var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
        var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
        var a = Convert.ToInt32(chArray[9].ToString());

        var b = num0 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9;
        var c = b % 11;

        return (c < 2 && a == c) || (c >= 2 && 11 - c == a);
    }

    #endregion

    /// <summary>
    ///     DomainAddress Extensions
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>

    #region DomainAddress

    public static bool IsDomainAddressValid(this string value)
    {
        var result = false;
        var splited = value.Trim().Split(".");

        result = (splited.Length > 1 || value.Contains("@"))
                 && !splited.Last().IsPersianString()
                 && splited.Last().Length > 1
                 && splited.All(x => x.Length > 0 && !x.Contains(" "));

        var startWith = !(value.StartsWith("http://") || value.StartsWith("https://") || value.StartsWith("www."));
        if (value.Contains("@"))
        {
            result = result && startWith;
        }
        else
        {
            var endWith = value.Contains('.') && value.Split('.').All(a => !a.IsNullOrEmpty());

            result = result && startWith && endWith;
        }

        return result;
    }

    public static bool DomainAddressHasAnyBadCharacter(this string value)
    {
        var badCharacters = new[]
        {
            '&', '‘', '*', '(', ')',
            ',', '!', '?', '^', ':', '\\', '/', ';', '+', '=',
            '<', '>', '|', '$', '€', '£', '#', ' ', '~', '`', '\''
        };

        var result = value.Any(x => badCharacters.Contains(x));

        return result;
    }

    public static string ConvertToIDN(this string domainName)
    {
        // تبدیل نام دامنه به فرمت IDN
        var idnDomain = WebUtility.UrlEncode(domainName);

        // تغییر "%20" به "+"
        idnDomain = idnDomain.Replace("%20", "+");

        // بازگرداندن نام دامنه به فرمت IDN
        return idnDomain;
    }

    public static string ConvertToOriginal(this string idnDomain)
    {
        // تبدیل نام دامنه از فرمت IDN به زبان اصلی
        var originalDomain = WebUtility.UrlDecode(idnDomain);

        // تغییر "+" به "%20"
        originalDomain = originalDomain.Replace("+", "%20");

        // بازگرداندن نام دامنه به زبان اصلی
        return originalDomain;
    }

    #endregion
}