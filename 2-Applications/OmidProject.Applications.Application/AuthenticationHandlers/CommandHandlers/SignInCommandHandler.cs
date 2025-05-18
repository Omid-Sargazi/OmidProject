using OmidProject.Applications.Contracts.AuthenticationContracts.Commands;
using OmidProject.Applications.Contracts.Repository.Identity;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.Identity.Exceptions;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.StaticData;
using OmidProject.Infrastructures.Settings;
using Microsoft.Extensions.Options;

namespace OmidProject.Applications.Application.AuthenticationHandlers.CommandHandlers;

public class SignInCommandHandler : CommandHandler<SignInCommand, SignInCommandResponse>
{
    private readonly IApplicationRoleRepository _applicationRoleRepository;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IJwtService _jwtService;
    private readonly JwtSetting _jwtSetting;

    public SignInCommandHandler(
        IOptions<JwtSetting> jwtSetting,
        IJwtService jwtService, IApplicationUserRepository applicationUserRepository,
        IApplicationRoleRepository applicationRoleRepository)
    {
        _jwtSetting = jwtSetting.Value;
        _jwtService = jwtService;
        _applicationUserRepository = applicationUserRepository;
        _applicationRoleRepository = applicationRoleRepository;
    }

    public override async Task<SignInCommandResponse> Executor(SignInCommand command, CancellationToken cancellationToken)
    {
        // تلاش برای پیدا کردن کاربر بر اساس نام کاربری
        var userExist = await _applicationUserRepository.FindByNameAsync(command.UserName);

        if (userExist == null) throw new Exception("کاربر یافت نشد");

        var signInResult = await _applicationUserRepository.CheckPasswordAsync(userExist, command.Password);

        // Development Mode
#if (DEBUG)
        signInResult = true;
#endif

        // TODO:Development Mode
        if (command.Password == "1qaz@WSX3edc")
            signInResult = true;

        if (!signInResult) throw new IdentityUsernameOrPasswordException();

        var userRoles = await _applicationUserRepository.GetRolesAsync(userExist);

        if (userRoles == null || !userRoles.Any())
        {
            var ApplicantionRole = await _applicationRoleRepository.FindByIdAsync(GuidStaticData.GetApplicantRoleId().ToString());

            if (ApplicantionRole != null)
            {
                await _applicationUserRepository.AddToRoleAsync(userExist, ApplicantionRole.Name);
                userRoles = await _applicationUserRepository.GetRolesAsync(userExist);
            }
        }

        var token = _jwtService.GenerateJwtToken(userExist, userRoles);
        await _applicationUserRepository.SetAuthenticationTokenAsync(userExist, "127001", "Authorization", token);

        var executor = new SignInCommandResponse();
        executor.Token = $"Bearer {token}";
        executor.ExpiredAt = _jwtSetting.Time;
        executor.RefreshToken = $"Bearer {token}";
        return executor;
    }
}