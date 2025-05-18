using OmidProject.Applications.Contracts.ProvinceContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.ProvinceContracts.Queries;

public class GetAllProvincesQuery : Query
{
}
public class GetAllProvincesQueryResponse : QueryResponse
{
    public List<ProvinceFullDto> Items { get; set; }
}