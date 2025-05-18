using Microsoft.AspNetCore.Identity;

namespace OmidProject.Domains.Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    /// کاربر پیشفرض
    /// </summary>
    public bool IsSuperAdmin { get; set; }
    public string FirstName{ get; set; }
    public string LastName { get; set; }
    public bool IsDeleted { get; protected set; }
    public DateTime? CreatedAt { get; set; }
    public void SetDelete()
    {
        IsDeleted = true;
    }
}