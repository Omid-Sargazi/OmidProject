using OmidProject.Applications.Application.Exceptions;
using OmidProject.Applications.Contracts.UserManagerContracts.Commands;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.UserManagerHandlers.CommandHandlers;

public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, CreateUserCommandResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    public override async Task<CreateUserCommandResponse> Executor(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = command.UserName,
            Email = command.Email
        };

        var userCreate = await _userManager.CreateAsync(user, command.Password);
        if (userCreate.Succeeded)
            return new CreateUserCommandResponse
            {
                Code = 1,
                Message = "User Has Been Created"
            };

        throw new IdentityException(null, userCreate.Errors.First().Description);
    }
}