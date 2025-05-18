using OmidProject.Applications.Contracts.FrontPageFormContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.FrontPageFormContracts.Queries;

public class GetFrontPageFormQuery : Query
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
}

public class GetFrontPageFormQueryResponse : QueryResponse
{
    public List<FrontPageFromDto> Items { get; set; }
}