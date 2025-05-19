using OmidProject.Applications.Contracts.City.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.CityContracts.Queries;

public class GetCityQuery : Query
{
    public int Id { get; set; }
}

public class GetCityQueryResponse : QueryResponse
{
    public CityDto Item { get; set; }
}