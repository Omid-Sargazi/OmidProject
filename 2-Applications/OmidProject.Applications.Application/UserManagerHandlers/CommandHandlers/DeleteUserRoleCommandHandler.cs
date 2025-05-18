using OmidProject.Applications.Application.Exceptions;
using OmidProject.Applications.Contracts.UserManagerContracts.Commands;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Applications.Application.UserManagerHandlers.CommandHandlers;

public class DeleteUserRoleCommandHandler : CommandHandler<DeleteUserRoleCommand, DeleteUserRoleCommandResponse>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteUserRoleCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }


    public override async Task<DeleteUserRoleCommandResponse> Executor(DeleteUserRoleCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == command.UserName);
        if (user == null)
            throw new IdentityException("کاربر مورد نظر وجود ندارد !", "User is not exist");

        var role = await _roleManager.FindByNameAsync(command.RoleName);

        if (role == null)
            throw new IdentityException("رول مورد نظر وجود ندارد !", "Role is not exist");

        var result = await _userManager.RemoveFromRoleAsync(user, command.RoleName);

        if (result.Succeeded)
            return new DeleteUserRoleCommandResponse
            {
                Message = "User Role has been deleted successfully"
            };

        throw new IdentityException(null, result.Errors.First().Description);
    }
}