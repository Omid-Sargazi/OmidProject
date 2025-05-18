using OmidProject.Applications.Application.Exceptions;
using OmidProject.Applications.Contracts.UserManagerContracts.Commands;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Applications.Application.UserManagerHandlers.CommandHandlers;

public class DeleteUserCommandHandler : CommandHandler<DeleteUserCommand, DeleteUserCommandResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    public override async Task<DeleteUserCommandResponse> Executor(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == command.UserName);
        if (user == null)
            return new DeleteUserCommandResponse
            {
                Message = "User is not exist"
            };

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
            return new DeleteUserCommandResponse
            {
                Message = "User has been deleted successfully"
            };

        throw new IdentityException(null, result.Errors.First().Description);
    }
}