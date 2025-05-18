using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace OmidProject.Host.CustomAttribute;

[AttributeUsage(AttributeTargets.Method)]
public class SemaphoreLockAttribute : Attribute, IAsyncActionFilter
{
    private static readonly SemaphoreSlim Semaphore = new(1, 1);

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Acquire the semaphore
        await Semaphore.WaitAsync();
        try
        {
            // Proceed with the action
            await next();
        }
        finally
        {
            // Release the semaphore
            Semaphore.Release();
        }
    }
}
