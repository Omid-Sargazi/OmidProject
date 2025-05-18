using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Frameworks.Contracts.Markers;

public interface IQueryHandler<in TQuery, TQueryResponse> where TQuery : Query where TQueryResponse : QueryResponse
{
    Task<TQueryResponse> Execute(TQuery query, CancellationToken cancellationToken);
}