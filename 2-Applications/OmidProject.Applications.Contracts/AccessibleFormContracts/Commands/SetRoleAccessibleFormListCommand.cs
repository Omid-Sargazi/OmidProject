using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Commands;

public class SetRoleAccessibleFormListCommand : Command
{
    public Guid RoleId { get; set; }

    public List<int> AccessibleFormIds { get; set; }

    //public Items<int,Guid> RoleIdsAndAccessibleFormIds { get; set; }
}

public class SetRoleAccessibleFormListCommandResponse : CommandResponse
{
}