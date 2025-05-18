using OmidProject.Applications.Contracts.AuthenticationContracts.Commands;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Domains.Domain.Identity.Exceptions;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.AuthenticationHandlers.CommandHandlers;

public class SignOutCommandHandler : CommandHandler<SignOutCommand, SignOutCommandResponse>
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public SignOutCommandHandler(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }


    public override async Task<SignOutCommandResponse> Executor(SignOutCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _signInManager.SignOutAsync();
            return new SignOutCommandResponse
            {
                Message = "You Signed Out Successfully"
            };
        }
        catch (Exception e)
        {
            throw new IdentitySignOutException();
        }
    }
}