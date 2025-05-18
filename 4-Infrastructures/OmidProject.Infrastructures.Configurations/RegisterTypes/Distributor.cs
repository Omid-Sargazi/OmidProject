using System.Collections;
using System.Security.Claims;
using OmidProject.Frameworks.Contracts.Abstracts;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace OmidProject.Infrastructures.Configurations.RegisterTypes;

public class Distributor(
    IServiceProvider serviceProvider,
    IHttpContextAccessor httpContextAccessor) : IDistributor
{
    public async Task<TCommandResponse> PushCommand<TCommand, TCommandResponse>(TCommand command,
        CancellationToken cancellationToken)
        where TCommand : Command
        where TCommandResponse : CommandResponse
    {
        var handler =
            serviceProvider.GetRequiredService(typeof(ICommandHandler<TCommand, TCommandResponse>)) as
                ICommandHandler<TCommand, TCommandResponse>;

        FixAllStringProperties(command);
        command.Metadata = GetMetadata();

        var commandResponse = await handler.Execute(command, cancellationToken);

        return commandResponse;
    }

    public async Task<TQueryResponse> PullQuery<TQuery, TQueryResponse>(TQuery query,
        CancellationToken cancellationToken)
        where TQuery : Query
        where TQueryResponse : QueryResponse
    {
        var handler =
            serviceProvider.GetService(typeof(IQueryHandler<TQuery, TQueryResponse>)) as
                IQueryHandler<TQuery, TQueryResponse>;

        FixAllStringProperties(query);
        query.Metadata = GetMetadata();

        var queryResponse = await handler.Execute(query, cancellationToken);

        return queryResponse;
    }

    private Metadata GetMetadata()
    {
        var result = new Metadata();

        if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.User.Claims.Any())
        {
            var claims = httpContextAccessor.HttpContext.User.Claims;

            var name = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

            var identifier = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            result.UserName = name.Value;

            result.UserId = Guid.Parse(identifier.Value);
        }
        else
        {
            result.UserId = Guid.Parse("C6D86FF5-C109-4948-B759-FE8F6ACD87EB");
            result.UserName = "public";
        }

        return result;
    }

    private void FixAllStringProperties(object? obj)
    {
        if (obj == null)
            return;

        var properties = obj.GetType().GetProperties();
        foreach (var property in properties)
            if (property.PropertyType == typeof(string))
            {
                var value = (string)property.GetValue(obj)!;
                if (!value.IsNullOrEmpty())
                {
                    // اعمال تابع خاص به رشته
                    value = value.FixPersianCharsAndNumbers();
                    property.SetValue(obj, value);
                }
            }
            else if (property.PropertyType.IsClass && property.PropertyType != typeof(Metadata))
            {
                if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    // اگر نوع فیلد IEnumerable است، تک تک آیتم‌های درون آن را بررسی و متد را بر روی هرکدام اعمال کنید
                    if (property.GetValue(obj) is IEnumerable enumerableObject)
                        foreach (var item in enumerableObject)
                            FixAllStringProperties(item);
                }
                else
                {
                    // اگر فیلد یک شیء است ولی نوع آن رشته نیست، متد را بر روی شیء دیگر فراخوانی می‌کنیم
                    FixAllStringProperties(property.GetValue(obj));
                }
            }
    }
}