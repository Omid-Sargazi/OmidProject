using OmidProject.Applications.Contracts.FrontPageFormContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.FrontPageFormContracts.Queries;

public class GetRoleFrontPageFormQuery : Query
{
    public Guid RoleId { get; set; }
}

public class GetRoleFrontPageFormQueryResponse : QueryResponse
{
    public List<RoleFrontPageFromDto> Items { get; set; }
}