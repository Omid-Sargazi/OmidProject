using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.General;

public class Advertisement : Entity<int>
{
    public Advertisement(string title, string description, string phoneNumber, decimal price, int districtId,
        int categoryId)
    {
        Title = title;
        Description = description;
        PhoneNumber = phoneNumber;
        Price = price;
        DistrictId = districtId;
        CategoryId = categoryId;
    }

    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public string PhoneNumber { get; protected set; }
    public decimal Price { get; protected set; }
    public int DistrictId { get; protected set; }
    public District District { get; set; } //navigational property
    public int CategoryId { get; protected set; }
    public Category Category { get; set; } //navigational property
    public List<AdvertisementImage> AdvertisementImages { get; set; }

    public void Update(string title, string description, string phoneNumber, decimal price, int districtId,
        int categoryId)
    {
        Title = title;
        Description = description;
        PhoneNumber = phoneNumber;
        Price = price;
        DistrictId = districtId;
        CategoryId = categoryId;
    }
}