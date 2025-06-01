using OmidProject.Applications.Contracts.CategoryContracts.Queries.DTOs;

namespace OmidProject.Applications.Contracts.AdvertisementContracts.Queries.DTOs;

public class AdvertisementDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public decimal Price { get; set; }
    public int DistrictId { get; set; }
    public string DistrictName { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public CategoryDto Category { get; set; }

    public List<Guid> ImageIds { get; set; }
}