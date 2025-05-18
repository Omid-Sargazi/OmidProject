using OmidProject.Applications.Application.Exceptions;
using OmidProject.Applications.Contracts.UserManagerContracts.Commands;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.UserManagerHandlers.CommandHandlers;

public class AddRoleToUserCommandHandler : CommandHandler<AddRoleToUserCommand, AddRoleToUserCommandResponse>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AddRoleToUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }


    public override async Task<AddRoleToUserCommandResponse> Executor(AddRoleToUserCommand command, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByNameAsync(command.UserRoleName);
        if (role == null)
            throw new IdentityException("رول مورد نظر وجود ندارد !", "Role is not exist");

        var user = await _userManager.FindByNameAsync(command.UserName);
        if (role == null)
            throw new IdentityException("کاربر مورد نظر وجود ندارد !", "User is not exist");

        var result = await _userManager.AddToRoleAsync(user, role.Name);
        if (result.Succeeded)
            return new AddRoleToUserCommandResponse
            {
                Code = 1,
                Message = "User Role Has Been Created"
            };

        throw new IdentityException(null, result.Errors.First().Description);
    }
}