using OmidProject.Applications.Contracts.CustomAttribute;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.FrontPageFormContracts.Commands;

public class EditFrontPageFormCommand : Command
{
    //[ExistsInDatabase("FrontPageForm", "Id", ErrorMessage = "شناسه وارد شده در پایگاه داده وجود ندارد.")]
    [PositiveIdAttribute("آیدی")] public int Id { get; set; }

    [RequiredProperty("عنوان صفحه")] public string Title { get; set; }

    public string Route { get; set; }
}

public class EditFrontPageFormCommandResponse : CommandResponse
{
}