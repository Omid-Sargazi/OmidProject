using OmidProject.Applications.Contracts.AdvertisementContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.AdvertisementImageContracts.Queries.DTOs;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface IAdvertisementImageService : IService
{
    AdvertisementImageDto ConvertTo(AdvertisementImage advertisementImage);
    List<AdvertisementImageDto> ConvertTo(List<AdvertisementImage> advertisements);
}