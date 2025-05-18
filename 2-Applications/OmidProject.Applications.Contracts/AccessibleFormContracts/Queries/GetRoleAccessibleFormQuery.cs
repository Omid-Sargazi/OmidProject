using OmidProject.Applications.Contracts.AccessibleFormContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Queries;

public class GetRoleAccessibleFormQuery : Query
{
    public Guid RoleId { get; set; }
}

public class GetRoleAccessibleFormQueryResponse : QueryResponse
{
    public List<RoleAccessibleFormDto> Items { get; set; }
}