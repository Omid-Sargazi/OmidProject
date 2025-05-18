using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.General;

public class District : Entity<int>
{
    public string Name { get; set; }

    public int CityId { get; set; }
    public City City { get; set; }//navigation property
    public List<Advertisement> Advertisements { get; set; }
}