using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.CityContracts.Queries;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.CityHandlers.QueryHandlers;

public class GetCityQueryHandler : IQueryHandler<GetCityQuery, GetCityQueryResponse>
{
    private readonly ICityRepository _cityRepository;
    private readonly ICityService _cityService;
    public GetCityQueryHandler(ICityRepository cityRepository, ICityService cityService)
    {
        _cityRepository = cityRepository;
        _cityService = cityService;
    }

    public async Task<GetCityQueryResponse> Execute(GetCityQuery query, CancellationToken cancellationToken)
    {
        Guard(query);
        var city = await _cityRepository.GetByIdAsync(query.Id);
        var result = new GetCityQueryResponse();
        result.Item = _cityService.ConvertTo(city);
        return result;
    }

    private void Guard(GetCityQuery query)
    {
        if (query.Id.IsInvalidId())
            throw new IdIsInvalidIdException();
    }
}