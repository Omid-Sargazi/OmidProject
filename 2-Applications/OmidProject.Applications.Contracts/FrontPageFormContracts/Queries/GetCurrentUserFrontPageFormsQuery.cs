using OmidProject.Applications.Contracts.FrontPageFormContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.FrontPageFormContracts.Queries;

public class GetCurrentUserFrontPageFormsQuery : Query
{
}

public class GetCurrentUserFrontPageFormsQueryResponse : QueryResponse
{
    public List<FrontPageFromDto> Items { get; set; }
}