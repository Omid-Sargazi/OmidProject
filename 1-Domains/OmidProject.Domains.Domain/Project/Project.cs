using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.Project;

public class Project : Entity<int>
{
    public Project()
    {
    }

    public Project(string name, int price)
    {
        Name = name;
        Price = price;
    }


    public string Name { get; protected set; }
    public int Price { get; protected set; }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetPrice(int price)
    {
        Price = price;
    }
}