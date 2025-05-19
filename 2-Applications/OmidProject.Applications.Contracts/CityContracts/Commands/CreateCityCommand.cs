using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
namespace OmidProject.Applications.Contracts.CityContracts.Commands;

public class CreateCityCommand : Command
{
    public string Name { get; set; }
}

public class CreateCityCommandReposne : CommandResponse
{
    public int Id { get; set; }
}