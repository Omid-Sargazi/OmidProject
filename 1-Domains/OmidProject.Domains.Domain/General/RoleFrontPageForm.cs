using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.General;

public class RoleFrontPageForm : Entity<int>
{
    public RoleFrontPageForm()
    {
    }

    public RoleFrontPageForm(Guid roleId, int frontPageFormId)
    {
        RoleId = roleId;
        FrontPageFormId = frontPageFormId;
    }

    public ApplicationRole ApplicantRole { get; protected set; }
    public Guid RoleId { get; protected set; }

    public FrontPageForm FrontPageForm { get; protected set; }
    public int FrontPageFormId { get; protected set; }

    public void Update(Guid roleId, int frontPageFormId)
    {
        RoleId = roleId;
        FrontPageFormId = frontPageFormId;
    }

    public void SetRoleId(Guid roleId)
    {
        RoleId = roleId;
    }

    public void SetFrontPageFormId(int frontPageFormId)
    {
        FrontPageFormId = frontPageFormId;
    }
}