using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Frameworks.Contracts.Markers;

public interface IDistributor
{
    Task<TCommandResponse> PushCommand<TCommand, TCommandResponse>(TCommand command,
        CancellationToken cancellationToken)
        where TCommand : Command where TCommandResponse : CommandResponse;

    Task<TQueryResponse> PullQuery<TQuery, TQueryResponse>(TQuery query, CancellationToken cancellationToken)
        where TQuery : Query where TQueryResponse : QueryResponse;
}