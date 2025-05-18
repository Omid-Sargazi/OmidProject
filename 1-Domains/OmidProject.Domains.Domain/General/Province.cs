using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.General;

public class Province : Entity<int>
{
    public Province(string name)
    {
        Name = name;
    }
    public string Name { get; protected set; }
    public List<City> Cities { get; set; }
}