using OmidProject.Applications.Contracts.City.Queries.DTOs;

namespace OmidProject.Applications.Contracts.ProvinceContracts.Queries.DTOs;

public class ProvinceFullDto : ProvinceDto
{
    public int CitiesCount { get; set; }
    public List<CityDto> Cities { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ShamsiCreatedAt { get; set; }
}