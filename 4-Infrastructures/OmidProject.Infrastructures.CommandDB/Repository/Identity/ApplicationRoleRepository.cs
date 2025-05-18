using OmidProject.Applications.Contracts.Repository.Identity;
using OmidProject.Domains.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Infrastructures.CommandDb.Repository.Identity;

public class ApplicationRoleRepository : IApplicationRoleRepository
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationRoleRepository(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<ApplicationRole> FindByNameAsync(string roleName)
    {
        var result = await _roleManager.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.NormalizedName == roleName.ToUpper());

        return result;
    }

    public async Task<ApplicationRole> FindByIdAsync(string roleId)
    {
        var result = await _roleManager.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(roleId));

        return result;
    }

    public async Task CreateAsync(ApplicationRole applicationRole)
    {
        await _roleManager.CreateAsync(applicationRole);
    }
}