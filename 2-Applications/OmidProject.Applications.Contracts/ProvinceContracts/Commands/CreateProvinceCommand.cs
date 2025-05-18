using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.ProvinceContracts.Commands;

public class CreateProvinceCommand : Command
{
    public string Name { get; set; }
}

public class CreateProvinceCommandResponse : CommandResponse
{
    public int Id { get; set; }
}