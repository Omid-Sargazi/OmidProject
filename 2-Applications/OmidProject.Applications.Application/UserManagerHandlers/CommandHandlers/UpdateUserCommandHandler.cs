using OmidProject.Applications.Application.Exceptions;
using OmidProject.Applications.Contracts.UserManagerContracts.Commands;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Applications.Application.UserManagerHandlers.CommandHandlers;

public class UpdateUserHandler : CommandHandler<UpdateUserCommand, UpdateUserCommandResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateUserHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    public override async Task<UpdateUserCommandResponse> Executor(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == command.UserName);

        if (user == null)
            throw new IdentityException("کاربر مورد نظر وجود ندارد !", "User is not exist !");

        user.UserName = command.List.UserName;
        user.NormalizedUserName = command.List.NormalizedUserName;
        user.Email = command.List.Email;
        user.EmailConfirmed = command.List.EmailConfirmed;
        user.PasswordHash = command.List.PasswordHash;
        user.SecurityStamp = command.List.SecurityStamp;
        user.ConcurrencyStamp = command.List.ConcurrencyStamp;
        user.PhoneNumber = command.List.PhoneNumber;
        user.PhoneNumberConfirmed = command.List.PhoneNumberConfirmed;
        user.TwoFactorEnabled = command.List.TwoFactorEnabled;
        user.LockoutEnabled = command.List.LockoutEnabled;
        user.AccessFailedCount = command.List.AccessFailedCount;


        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded)
            return new UpdateUserCommandResponse
            {
                Message = "User has been updated successfully"
            };

        throw new IdentityException(null, result.Errors.First().Description);
    }
}