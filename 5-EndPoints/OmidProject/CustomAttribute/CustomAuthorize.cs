using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Frameworks.Contracts.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace OmidProject.Host.CustomAttribute;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class CustomAuthorize : Attribute, IAsyncAuthorizationFilter
{
    private IRoleAccessibleFormService _roleAccessibleFormService;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (HasAllowAnonymous(context) || IsSuperAdmin(context))
            return;

        _roleAccessibleFormService =
            (IRoleAccessibleFormService)context.HttpContext.RequestServices.GetRequiredService(
                typeof(IRoleAccessibleFormService));

        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var currentUserRoleNames = context.HttpContext.User.Claims
            .Where(w => w.Type == ClaimTypes.Role)
            .Select(s => s.Value)
            .ToList();

        var route = GetRoute(context.HttpContext.Request.Path.Value?.ToLowerInvariant());

        if (!await HasAccess(currentUserRoleNames, route)) context.Result = new ForbidResult();
    }

    private static bool IsSuperAdmin(AuthorizationFilterContext context)
    {
        var result = context.HttpContext.User.Claims.Any(w => w.Type == ClaimTypes.Actor && w.Value == true.ToString());
        return result;
    }

    private static bool HasAllowAnonymous(AuthorizationFilterContext context)
    {
        Contract.Assert(context != null);

        return context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
    }

    private static string GetRoute(string requestPath)
    {
        var result = "";

        var splitPath = requestPath.Split('/');

        if (splitPath.Length >= 2)
            result = $"/{splitPath[^2]}/{splitPath[^1]}";

        return result;
    }

    private async Task<bool> HasAccess(List<string> currentUserRoleNames, string route)
    {
        var roleAccessibleForms = await _roleAccessibleFormService.ReadAllFromCache();

        var result = roleAccessibleForms
            .Any(a => currentUserRoleNames.Contains(a.ApplicantRole.Name ?? "") &&
                      a.AccessibleForm.Route.ToLower() == route);

        return result;
    }

    private static ContentLanguage GetContentLanguage(HttpContext context)
    {
        var result = ContentLanguage.Persian;

        if (!context.Request.Headers.ContainsKey("language")) return result;
        var num = Convert.ToInt32(context.Request.Headers["language"].First());

        if (Enum.IsDefined(typeof(ContentLanguage), num)) result = (ContentLanguage)num;

        return result;
    }
}