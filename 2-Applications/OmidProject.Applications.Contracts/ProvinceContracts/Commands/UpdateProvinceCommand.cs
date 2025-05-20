using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.ProvinceContracts.Commands;

public class UpdateProvinceCommand : Command
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class UpdateProvinceCommandResponse : CommandResponse
{

}