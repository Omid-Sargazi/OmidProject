using OmidProject.Applications.Contracts.AdvertisementContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.CategoryContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.Service;

public class AdvertisementService : IAdvertisementService
{
    private readonly ICategoryService _categoryService;

    public AdvertisementService(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public AdvertisementDto ConvertTo(Advertisement advertisement)
    {
        var result = advertisement.MapTo<AdvertisementDto>();

        result.DistrictName = advertisement.District != null ? advertisement.District.Name : "";
        result.CategoryName = advertisement.Category != null ? advertisement.Category.Name : "";
        result.Category = advertisement.Category != null ? _categoryService.ConvertTo(advertisement.Category) : new CategoryDto();

        result.ImageIds = advertisement.AdvertisementImages != null
            ? advertisement.AdvertisementImages.Select(x => x.ImageId).ToList()
            : new List<Guid>();

        return result;
    }

    public List<AdvertisementDto> ConvertTo(List<Advertisement> advertisements)
    {
        var result = advertisements.Select(ConvertTo).ToList();
        return result;
    }
}