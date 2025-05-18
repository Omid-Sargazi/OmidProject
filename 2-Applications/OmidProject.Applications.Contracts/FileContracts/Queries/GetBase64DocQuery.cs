using OmidProject.Applications.Contracts.FileContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.FileContracts.Queries;

public class GetBase64DocQuery : Query
{
    public Guid DocumentId { get; set; }
}

public class GetBase64DocQueryResponse : QueryResponse
{
    public GetFileDto Item { get; set; }
}