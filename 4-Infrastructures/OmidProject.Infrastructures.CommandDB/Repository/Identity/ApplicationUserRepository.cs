using OmidProject.Applications.Contracts.Repository.Identity;
using OmidProject.Domains.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Infrastructures.CommandDb.Repository.Identity;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserRepository(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<ApplicationUser> FindByNameAsync(string userName)
    {
        var result = await _userManager.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.NormalizedUserName == userName.ToUpper());

        return result;
    }

    public async Task<ApplicationUser> FindByIdAsync(string id)
    {
        var result = await _userManager.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

        return result;
    }

    public async Task<bool> CheckPasswordAsync(ApplicationUser applicantUser, string password)
    {
        var result = await _userManager.CheckPasswordAsync(applicantUser, password);
        return result;
    }

    public async Task<IList<string>> GetRolesAsync(ApplicationUser applicantUser)
    {
        var result = await _userManager.GetRolesAsync(applicantUser);
        return result;
    }

    public async Task AddToRoleAsync(ApplicationUser applicantUser, string roleName)
    {
        await _userManager.AddToRoleAsync(applicantUser, roleName);
    }

    public async Task SetAuthenticationTokenAsync(ApplicationUser applicantUser, string token, string? loginProvider,
        string? tokenName)
    {
        await _userManager.SetAuthenticationTokenAsync(applicantUser, loginProvider, tokenName, token);
    }

    public async Task<IdentityResult> CreateAsync(ApplicationUser applicantUser, string password)
    {
        var result = await _userManager.CreateAsync(applicantUser, password);
        return result;
    }
}