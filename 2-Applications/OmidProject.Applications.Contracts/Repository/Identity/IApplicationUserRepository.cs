using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Markers;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Contracts.Repository.Identity;

public interface IApplicationUserRepository : IRepository
{
    Task<ApplicationUser> FindByNameAsync(string userName);
    Task<ApplicationUser> FindByIdAsync(string id);
    Task<bool> CheckPasswordAsync(ApplicationUser applicantUser, string password);
    Task<IList<string>> GetRolesAsync(ApplicationUser applicantUser);
    Task AddToRoleAsync(ApplicationUser applicantUser, string roleName);

    Task SetAuthenticationTokenAsync(ApplicationUser applicantUser, string token, string? loginProvider, string? tokenName);
    Task<IdentityResult> CreateAsync(ApplicationUser applicantUser, string password);
}