using System.Net.Http.Json;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.IntegrationTests.Configuration.Helpers;

/// <summary>
///     Extension methods for creating JSON content from command and query objects.
/// </summary>
public static class JsonContents
{
    /// <summary>
    ///     Creates JSON content from a command object.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command, which must inherit from the Command base class.</typeparam>
    /// <param name="command">The command object to be serialized into JSON content.</param>
    /// <returns>A JsonContent representation of the command.</returns>
    public static JsonContent CreateCommandJsonContent<TCommand>(this TCommand command)
        where TCommand : Command 
    {
        return JsonContent.Create(command);
    }

    /// <summary>
    ///     Creates JSON content from a query object.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query, which must inherit from the Query base class.</typeparam>
    /// <param name="query">The query object to be serialized into JSON content.</param>
    /// <returns>A JsonContent representation of the query.</returns>
    public static JsonContent CreateJsonContentFromQuery<TQuery>(this TQuery query)
        where TQuery : Query
    {
        return JsonContent.Create(query);
    }
}