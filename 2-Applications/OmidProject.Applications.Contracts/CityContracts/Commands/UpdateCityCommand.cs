using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.CityContracts.Commands;

public class UpdateCityCommand : Command
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProvinceId { get; set; }
}

public class UpdateCityCommandResponse : CommandResponse
{
   public int Id { get; set; }
}