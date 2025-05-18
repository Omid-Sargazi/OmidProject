using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.General;

public class City : Entity<int>
{
    public string Name { get; set; }

    public int ProvinceId { get; set; }
    public Province Province { get; set; }//navigation property
    public List<District> Districts { get; set; }
}

   

