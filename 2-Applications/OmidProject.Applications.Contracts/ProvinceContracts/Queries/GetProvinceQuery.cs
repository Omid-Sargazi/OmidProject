using OmidProject.Applications.Contracts.ProvinceContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.ProvinceContracts.Queries;

public class GetProvinceQuery : Query
{
    public int Id { get; set; }
}

public class GetProvinceQueryResponse : QueryResponse
{
    public ProvinceFullDto Item { get; set; }
}