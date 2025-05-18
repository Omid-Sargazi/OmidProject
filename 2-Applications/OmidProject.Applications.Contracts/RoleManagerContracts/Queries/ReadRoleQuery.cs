using OmidProject.Applications.Contracts.RoleManagerContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.RoleManagerContracts.Queries;

public class ReadRoleQuery : Query
{
}

public class ReadRoleQueryResponse : QueryResponse
{
    public ReadRoleQueryResponse()
    {
        List = new List<RoleDto>();
    }

    public List<RoleDto> List { get; set; }
}