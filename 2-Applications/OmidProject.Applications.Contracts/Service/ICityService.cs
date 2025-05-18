using OmidProject.Applications.Contracts.City.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface ICityService : IService
{
    CityDto ConvertTo(Domains.Domain.General.City city);
    List<CityDto> ConvertTo(List<Domains.Domain.General.City> cities);
}