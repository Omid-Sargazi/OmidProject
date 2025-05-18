using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.General;

public class RoleAccessibleForm : Entity<int>
{
    public RoleAccessibleForm()
    {
    }

    public RoleAccessibleForm(Guid roleId, int accessibleFormId)
    {
        RoleId = roleId;
        AccessibleFormId = accessibleFormId;
    }

    public ApplicationRole ApplicantRole { get; protected set; }
    public Guid RoleId { get; protected set; }

    public AccessibleForm AccessibleForm { get; protected set; }
    public int AccessibleFormId { get; protected set; }

    public void Update(Guid roleId, int accessibleFormId)
    {
        RoleId = roleId;
        AccessibleFormId = accessibleFormId;
    }

    public void SetRoleId(Guid roleId)
    {
        RoleId = roleId;
    }

    public void SetAccessibleFormId(int accessibleFormId)
    {
        AccessibleFormId = accessibleFormId;
    }
}