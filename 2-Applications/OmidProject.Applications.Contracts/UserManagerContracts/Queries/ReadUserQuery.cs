using OmidProject.Applications.Contracts.UserManagerContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.UserManagerContracts.Queries;

public class ReadUserQuery : Query
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
}

public class ReadUserQueryResponse : QueryResponse
{
    public List<ApplicationUserDto> Items { get; set; }
}