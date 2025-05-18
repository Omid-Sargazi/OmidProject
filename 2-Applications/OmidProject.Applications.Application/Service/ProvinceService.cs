using OmidProject.Applications.Contracts.City.Queries.DTOs;
using OmidProject.Applications.Contracts.ProvinceContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.Service;

public class ProvinceService : IProvinceService
{
    private readonly ICityService _cityService;
    private readonly IProvinceRepository _provinceRepository;

    public ProvinceService(ICityService cityService)
    {
        _cityService = cityService;
    }

    public ProvinceFullDto ConvertTo(Province province)
    {
        var result = province.MapTo<ProvinceFullDto>();

        result.CitiesCount = province.Cities != null ? province.Cities.Count() : 0;
        result.ShamsiCreatedAt = province.CreatedAt.ToPersianDateTime();
        result.Cities = province.Cities != null ? _cityService.ConvertTo(province.Cities) : new List<CityDto>();

        return result;
    }

    public List<ProvinceFullDto> ConvertTo(List<Province> provinces)
    {
        var result = provinces.Select(ConvertTo).ToList();

        return result;
    }

    //public Province GetOrCreateProvince(string name)
    //{
    //    var province = _provinceRepository.GetById(id);
    //    if (province != null)
    //        return province;
    //    province = new Province(name);
    //    _provinceRepository.Add(province);
    //    return province;
    //}
}