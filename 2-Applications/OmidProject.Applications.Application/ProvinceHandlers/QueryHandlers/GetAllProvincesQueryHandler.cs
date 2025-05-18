using OmidProject.Applications.Contracts.ProvinceContracts.Queries;
using OmidProject.Applications.Contracts.ProvinceContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.ProvinceHandlers.QueryHandlers;

public class GetAllProvincesQueryHandler : IQueryHandler<GetAllProvincesQuery, GetAllProvincesQueryResponse>
{

    private readonly IProvinceRepository _ProvinceRepository;
    private readonly IProvinceService _provinceService;

    public GetAllProvincesQueryHandler(IProvinceRepository provinceRepository, IProvinceService provinceService)
    {
        _ProvinceRepository = provinceRepository;
        _provinceService = provinceService;
    }

    public async Task<GetAllProvincesQueryResponse> Execute(GetAllProvincesQuery query, CancellationToken cancellationToken)
    {
        var provinces = await _ProvinceRepository.GetAllAsync();

        var result = new GetAllProvincesQueryResponse();
        result.Items = _provinceService.ConvertTo(provinces);
        return result;
    }
}