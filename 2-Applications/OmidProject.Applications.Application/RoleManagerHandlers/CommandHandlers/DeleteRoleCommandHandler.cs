using OmidProject.Applications.Contracts.RoleManagerContracts.Commands;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Domains.Domain.Identity.Exceptions;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.RoleManagerHandlers.CommandHandlers;

public class DeleteRoleCommandHandler : CommandHandler<DeleteRoleCommand, DeleteRoleCommandResponse>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public DeleteRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }


    public override async Task<DeleteRoleCommandResponse> Executor(DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        var roll = await _roleManager.FindByNameAsync(command.RollName);
        if (roll == null)
            throw new DeleteRoleException("Role Does Not Exist");

        var result = await _roleManager.DeleteAsync(roll);

        if (result.Succeeded)
            return new DeleteRoleCommandResponse
            {
                Message = "Role Has Been Deleted Successfully"
            };
        throw new DeleteRoleException(result.Errors.First().ToString());
    }
}