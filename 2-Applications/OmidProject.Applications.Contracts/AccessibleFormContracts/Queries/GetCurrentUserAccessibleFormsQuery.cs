using OmidProject.Applications.Contracts.AccessibleFormContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Queries;

public class GetCurrentUserAccessibleFormsQuery : Query
{

}
public class GetCurrentUserAccessibleFormsQueryResponse : QueryResponse
{
    public List<AccessibleFormDto> Items{ get; set; }
}