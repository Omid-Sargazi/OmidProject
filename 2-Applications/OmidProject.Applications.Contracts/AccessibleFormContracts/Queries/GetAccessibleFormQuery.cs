using OmidProject.Applications.Contracts.AccessibleFormContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Queries;

public class GetAccessibleFormQuery : Query
{
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
}

public class GetAccessibleFormQueryResponse : QueryResponse
{
    public List<AccessibleFormDto> Items { get; set; }
}