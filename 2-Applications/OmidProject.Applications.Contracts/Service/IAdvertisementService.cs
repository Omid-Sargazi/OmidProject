using OmidProject.Applications.Contracts.AdvertisementContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.ProvinceContracts.Queries.DTOs;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface IAdvertisementService : IService
{
    AdvertisementDto ConvertTo(Advertisement advertisement);
    List<AdvertisementDto> ConvertTo(List<Advertisement> advertisements);
}