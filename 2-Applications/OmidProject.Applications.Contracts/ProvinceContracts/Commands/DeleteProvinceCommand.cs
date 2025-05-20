using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.ProvinceContracts.Commands;

public class DeleteProvinceCommand : Command
{
    public int Id { get; set; }
   
}
public class DeleteProvinceCommandResponse : CommandResponse
{

}

