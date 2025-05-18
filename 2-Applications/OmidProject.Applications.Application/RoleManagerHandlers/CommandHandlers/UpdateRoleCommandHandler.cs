using OmidProject.Applications.Contracts.RoleManagerContracts.Commands;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Domains.Domain.Identity.Exceptions;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.RoleManagerHandlers.CommandHandlers;

public class UpdateRoleCommandHandler : CommandHandler<UpdateRoleCommand, UpdateRoleCommandResponse>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public UpdateRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }


    public override async Task<UpdateRoleCommandResponse> Executor(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByNameAsync(command.OldRoleName);

        if (role == null)
            throw new DeleteRoleException("Role Does Not Exist");

        role.Name = command.NewRoleName;

        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
            return new UpdateRoleCommandResponse
            {
                Message = "Role Has Been Updated Successfully"
            };
        throw new DeleteRoleException(result.Errors.First().ToString());
    }
}