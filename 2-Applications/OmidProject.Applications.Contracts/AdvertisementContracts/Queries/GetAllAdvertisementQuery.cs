using OmidProject.Applications.Contracts.AdvertisementContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.AdvertisementContracts.Queries;

public class GetAllAdvertisementQuery : Query
{
}

public class GetAllAdvertisementQueryResponse : QueryResponse
{
    public List<AdvertisementDto> Items { get; set; }
}