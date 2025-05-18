using OmidProject.Applications.Contracts.City.Queries.DTOs;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.Service;

public class CityService : ICityService
{

    public CityDto ConvertTo(City city)
    {
        var result = city.MapTo<CityDto>();

        result.ProvinceName = city.Province != null ? city.Province.Name : "";

        return result;
    }

    public List<CityDto> ConvertTo(List<City> cities)
    {
        var result = cities.Select(ConvertTo).ToList();
        return result;
    }
}