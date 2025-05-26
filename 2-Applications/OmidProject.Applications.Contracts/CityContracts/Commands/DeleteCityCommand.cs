using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.CityContracts.Commands;

public class DeleteCityCommand : Command
{
   public int Id { get; set; }
   public string Name { get; set; }
}

public class DeleteCityCommandResponse : CommandResponse
{
    
}