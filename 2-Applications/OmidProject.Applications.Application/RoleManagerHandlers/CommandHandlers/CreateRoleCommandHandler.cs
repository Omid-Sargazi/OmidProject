using OmidProject.Applications.Contracts.RoleManagerContracts.Commands;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Domains.Domain.Identity.Exceptions;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.RoleManagerHandlers.CommandHandlers;

public class CreateRoleCommandHandler : CommandHandler<CreateRoleCommand, CreateRollCommandResponse>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }


    public override async Task<CreateRollCommandResponse> Executor(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var roll = new ApplicationRole()
        {
            Name = command.RollName
        };
        var result = await _roleManager.CreateAsync(roll);

        if (result.Succeeded)
            return new CreateRollCommandResponse
            {
                Message = "Role Has Been Created Successfully"
            };
        throw new CreateRoleException(result.Errors.First().Description);
    }
}