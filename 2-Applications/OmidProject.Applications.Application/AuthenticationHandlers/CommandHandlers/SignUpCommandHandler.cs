using OmidProject.Applications.Application.Exceptions;
using OmidProject.Applications.Contracts.AuthenticationContracts.Commands;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.AuthenticationHandlers.CommandHandlers;

public class SignUpCommandHandler : CommandHandler<SignUpCommand, SignUpCommandResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public SignUpCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    public override async Task<SignUpCommandResponse> Executor(SignUpCommand command, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = command.UserName,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName
        };

        var userCreate = await _userManager.CreateAsync(user, command.Password);
        if (userCreate.Succeeded)
            return new SignUpCommandResponse
            {
                Message = "کاربر با موفقیت ساخته شد."
            };


        throw new IdentityException(null, userCreate.Errors.First().Description);
    }
}