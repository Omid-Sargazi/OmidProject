using OmidProject.Applications.Contracts.AdvertisementContracts.Queries;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Application.AdvertisementHandlers.QueryHandlers;

public class GetAllAdvertisementQueryHandler : IQueryHandler<GetAllAdvertisementQuery, GetAllAdvertisementQueryResponse>
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly IAdvertisementService _advertisementService;

    public GetAllAdvertisementQueryHandler(IAdvertisementRepository advertisementRepository,
        IAdvertisementService advertisementService)
    {
        _advertisementRepository = advertisementRepository;
        _advertisementService = advertisementService;
    }

    public async Task<GetAllAdvertisementQueryResponse> Execute(GetAllAdvertisementQuery query,
        CancellationToken cancellationToken)
    {
        var advertisements = await _advertisementRepository.GetAllAsync(true);

        var result = new GetAllAdvertisementQueryResponse();
        result.Items = _advertisementService.ConvertTo(advertisements);
        return result;
    }
}