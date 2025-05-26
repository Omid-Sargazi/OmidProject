using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.General;

public class City : Entity<int>
{
    public City(string name, int provinceId)
    {
        Name = name;
        ProvinceId = provinceId;
    }

    public string Name { get; protected set; }
    public int ProvinceId { get; protected set; }
    public Province Province { get; set; }//navigation property
    public List<District> Districts { get; set; }

    public void Update(string name, int provinceId)
    
    {
        Name = name;
        ProvinceId = provinceId;
    }
}



