using OmidProject.Applications.Contracts.SystemMessageContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.SystemMessageContracts.Queries;

public class ReadUserRoleQuery : Query
{
    public string? UserName { get; set; }
    public string? RoleName { get; set; }
}

public class ReadUserRoleQueryResponse : QueryResponse
{
    public ReadUserRoleQueryResponse()
    {
        Items = new List<AspNetUserRolesDto>();
    }

    public List<AspNetUserRolesDto> Items { get; set; }

}