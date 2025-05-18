using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.Repository;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;
using OmidProject.Frameworks.Contracts.Common;
using OmidProject.Frameworks.Contracts.Common.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OmidProject.Host.Middleware;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private ISystemMessageRepository _systemErrorRepository;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ISystemMessageRepository systemErrorRepository)
    {
        _systemErrorRepository = systemErrorRepository;
        try
        {
            await _next(context);
        }
        catch (BusinessException bex)
        {
            await HandleExceptionAsync(context, bex);
        }
        catch (ApplicationException bex)
        {
            await HandleExceptionAsync(context, bex);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task<Task> HandleExceptionAsync(HttpContext context, BusinessException exception)
    {
        context.Response.ContentType = "application/json";
        string result;
        if (exception is BusinessException)
        {
            var contentLanguage = GetContentLanguage(context);

            result = new ApiResult
            {
                HasError = true,
                Message = await GetMessage(exception, contentLanguage),
                Code = (int) HttpStatusCode.Conflict
            }.ToString();
            context.Response.StatusCode = (int) HttpStatusCode.Conflict;
        }
        else
        {
            result = new ApiResult
            {
                HasError = true,
                Message = new StructureMessage
                {
                    Message = new[] {"Runtime Error"}
                },
                Code = (int) HttpStatusCode.BadRequest
            }.ToString();
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        }

        return context.Response.WriteAsync(result);
    }

    private async Task<Task> HandleExceptionAsync(HttpContext context, ApplicationException exception)
    {
        context.Response.ContentType = "application/json";

        string result;
        if (exception is ApplicationException)
        {
            var contentLanguage = GetContentLanguage(context);

            result = new ApiResult
            {
                HasError = true,
                Message = exception.Message,
                Code = (int) HttpStatusCode.Conflict
            }.ToString();

            context.Response.StatusCode = (int) (exception.Data["StatusCode"] ?? HttpStatusCode.Conflict);
        }
        else
        {
            result = new ApiResult
            {
                HasError = true,
                Message = new StructureMessage
                {
                    Message = new[] {"Runtime Error"}
                },
                Code = (int) HttpStatusCode.BadRequest
            }.ToString();
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        }

        return context.Response.WriteAsync(result);
    }

    public async Task<Task> HandleExceptionAsync(HttpContext context, Exception exception)
    {
        string result;
        context.Response.ContentType = "application/json";
        if (exception.InnerException == null)
        {
            result = new ApiResult
            {
                HasError = true,
                Message = new StructureMessage
                {
                    Message = new[] {exception.Message}
                },
                Code = (int) HttpStatusCode.InternalServerError
            }.ToString();
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }

        if (exception.InnerException.GetType() == typeof(BusinessException))
            return HandleExceptionAsync(context, exception.InnerException);

        result = new ApiResult
        {
            HasError = true,
            Message = new StructureMessage
            {
                Message = new[] {exception.Message}
            },
            Code = (int) HttpStatusCode.InternalServerError
        }.ToString();
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        return context.Response.WriteAsync(result);
    }

    public async Task<Task> HandleExceptionAsync(HttpContext context, ValidationException exception)
    {
        string result;
        var contentLanguage = GetContentLanguage(context);
        context.Response.ContentType = "application/json";
        if (exception.InnerException == null)
        {
            result = new ApiResult
            {
                HasError = true,
                Message = new ValidationExceptionStructureMessage
                {
                    Message = exception.Errors.Select(x => x.ErrorMessage).ToArray(),
                    MessageLanguage = contentLanguage
                },
                Code = (int) HttpStatusCode.Conflict
            }.ToString();
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }

        if (exception.InnerException.GetType() == typeof(BusinessException))
            return HandleExceptionAsync(context, exception.InnerException);

        result = new ApiResult
        {
            HasError = true,
            Message = new ValidationExceptionStructureMessage
            {
                Message = exception.Errors.Select(x => x.ErrorMessage).ToArray(),
                MessageLanguage = contentLanguage
            },
            Code = (int) HttpStatusCode.InternalServerError
        }.ToString();
        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        return context.Response.WriteAsync(result);
    }

    private ContentLanguage GetContentLanguage(HttpContext context)
    {
        var result = ContentLanguage.Persian;

        if (context.Request.Headers.ContainsKey("language"))
        {
            var num = Convert.ToInt32(context.Request.Headers["language"].First());

            if (Enum.IsDefined(typeof(ContentLanguage), num)) result = (ContentLanguage) num;
        }

        return result;
    }

    private async Task<dynamic> GetMessage(BusinessException exception, ContentLanguage contentLanguage)
    {
        var found =
            await _systemErrorRepository.GetDataMessageByCodeAndType(exception.Code, TypeSystemMessage.Error,
                contentLanguage);

        var structureMessage = new StructureMessage();

        if (found != null)
            structureMessage = new StructureMessage
                {MessageLanguage = found.MessageLanguage, Message = new[] {found.Message}};
        //else
        //    switch (contentLanguage)
        //    {
        //        case ContentLanguage.English:
        //            structureMessage = new StructureMessage
        //                {MessageLanguage = contentLanguage, Message = exception.EnglishMessage};
        //            break;
        //        case ContentLanguage.Persian:
        //            structureMessage = new StructureMessage
        //                {MessageLanguage = contentLanguage, Message = exception.PersianMessage};
        //            break;
        //        default:
        //            structureMessage = new StructureMessage
        //                {MessageLanguage = ContentLanguage.Persian, Message = exception.PersianMessage};
        //            break;
        //    }
        else
            structureMessage = new StructureMessage
            {
                Message = new[] {exception.Message},
                MessageLanguage = contentLanguage
            };

        return new JsonResult(structureMessage).Value;
    }
}