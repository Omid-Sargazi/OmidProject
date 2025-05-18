using OmidProject.Applications.Contracts.ProvinceContracts.Queries.DTOs;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface IProvinceService : IService
{
    ProvinceFullDto ConvertTo(Province province);
    List<ProvinceFullDto> ConvertTo(List<Province> provinces);
}