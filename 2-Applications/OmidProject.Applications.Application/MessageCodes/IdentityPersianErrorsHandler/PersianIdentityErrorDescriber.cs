﻿using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.MessageCodes.IdentityPersianErrorsHandler;

public class PersianIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError()
        {
            Code = nameof(DuplicateEmail),
            Description = $"ایمیل '{email}' قبلا توسط شخص دیگری انتخاب شده است@The"
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError()
        {
            Code = nameof(DuplicateUserName),
            Description =
                $"نام کاربری {userName} قبلا توسط شخص دیگری انتخاب شده است@The {userName} UserName is already taken by another person"
        };
    }

    public override IdentityError InvalidEmail(string email)
    {
        return new IdentityError()
        {
            Code = nameof(InvalidEmail),
            Description = $"ایمیل '{email}' ، یک ایمیل معتبر نیست"
        };
    }

    public override IdentityError DuplicateRoleName(string role)
    {
        return new IdentityError()
        {
            Code = nameof(DuplicateRoleName),
            Description = $"مقام '{role}' قبلا ثبت شده است"
        };
    }

    public override IdentityError InvalidRoleName(string role)
    {
        return new IdentityError()
        {
            Code = nameof(InvalidRoleName),
            Description = $"نام '{role}' معتبر نیست"
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError()
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "رمز عبور باید حداقل دارای یک عدد باشد"
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError()
        {
            Code = nameof(PasswordRequiresLower),
            Description = "رمز عبور باید حداقل دارای یک کاراکتر انگلیسی کوچک باشد ('a'-'z')"
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError()
        {
            Code = nameof(PasswordRequiresUpper),
            Description = "رمز عبور باید حداقل دارای یک کاراکتر انگلیسی بزرگ باشد ('A'-'Z')"
        };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError()
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = "رمز عبور باید حداقل دارای یک کاراکتر ویژه باشد مثل '@#%^&'"
        };
    }

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
    {
        return new IdentityError()
        {
            Code = nameof(PasswordRequiresUniqueChars),
            Description = $"رمز عبور باید حداقل دارای {uniqueChars} کاراکتر منحصر به فرد باشد"
        };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError()
        {
            Code = nameof(PasswordTooShort),
            Description = $"رمز عبور نباید کمتر از {length} کاراکتر باشد"
        };
    }

    public override IdentityError InvalidUserName(string userName)
    {
        return new IdentityError()
        {
            Code = nameof(InvalidUserName),
            Description = $"نام کاربری '{userName}' معتبر نیست، نام کاربری فقط میتواند دارای حروف یا عدد باشد"
        };
    }

    public override IdentityError UserNotInRole(string role)
    {
        return new IdentityError()
        {
            Code = nameof(UserNotInRole),
            Description = $"کاربر در مقام '{role}' نیست"
        };
    }

    public override IdentityError UserAlreadyInRole(string role)
    {
        return new IdentityError()
        {
            Code = nameof(UserAlreadyInRole),
            Description = $"کاربر در مقام '{role}' است"
        };
    }

    public override IdentityError DefaultError()
    {
        return new IdentityError()
        {
            Code = nameof(DefaultError),
            Description = "خطای پیشبینی نشده رخ داد"
        };
    }
}