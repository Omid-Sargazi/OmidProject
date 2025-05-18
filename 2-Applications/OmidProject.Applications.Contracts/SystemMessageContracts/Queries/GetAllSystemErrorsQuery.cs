using OmidProject.Applications.Contracts.SystemMessageContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.SystemMessageContracts.Queries;

public class GetAllSystemErrorsQuery : Query
{
}

public class GetAllSystemErrorsQueryResponse : QueryResponse
{
    public GetAllSystemErrorsQueryResponse()
    {
        List = new List<SystemErrorDto>();
    }

    public List<SystemErrorDto> List { get; set; }
}