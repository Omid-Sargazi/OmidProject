using OmidProject.Applications.Contracts.AdvertisementContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.Service;

public class AdvertisementService : IAdvertisementService
{
    public AdvertisementDto ConvertTo(Advertisement advertisement)
    {
        var result = advertisement.MapTo<AdvertisementDto>();

        result.DistrictName = advertisement.District != null ? advertisement.District.Name : "";
        result.CategoryName = advertisement.Category != null ? advertisement.Category.Name : "";

        return result;
    }

    public List<AdvertisementDto> ConvertTo(List<Advertisement> advertisements)
    {
        var result = advertisements.Select(ConvertTo).ToList();
        return result;
    }
}