using OmidProject.Domains.Domain.General;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Domains.Domain.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole()
    {

    }

    public ApplicationRole(string roleName) : base(roleName)
    {

    }

    public List<RoleAccessibleForm> RoleAccessibleForms { get; set; }
    public List<RoleFrontPageForm> RoleFrontPageForms { get; set; }
}