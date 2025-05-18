using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.AuthenticationContracts.Commands;
using OmidProject.Frameworks.Contracts.Markers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OmidProject.Host.Controllers.Authentication;

[AllowAnonymous]
public class AuthenticationController(IDistributor distributor) : MainController(distributor)
{
    private readonly IDistributor _distributor = distributor;

    // Constructor to inject dependencies

    /// <summary>
    ///     Handles user sign-up requests.
    /// </summary>
    /// <param name="signUpCommand">The command containing sign-up details.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>An IActionResult indicating the outcome of the sign-up operation.</returns>
    [Description("ثبت نام")]
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpCommand signUpCommand, CancellationToken cancellationToken)
    {
        var result =
            await _distributor.PushCommand<SignUpCommand, SignUpCommandResponse>(signUpCommand, cancellationToken);
        return OkApiResult(result);
    }

    /// <summary>
    ///     Handles user sign-in requests.
    /// </summary>
    /// <param name="signInCommand">The command containing sign-in details.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>An IActionResult indicating the outcome of the sign-in operation.</returns>
    [Description("ورود")]
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInCommand signInCommand, CancellationToken cancellationToken)
    {
        var result =
            await _distributor.PushCommand<SignInCommand, SignInCommandResponse>(signInCommand, cancellationToken);
        return OkApiResult(result);
    }

    /// <summary>
    ///     Handles user sign-out requests.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>An IActionResult indicating the outcome of the sign-out operation.</returns>
    [Description("خروج")]
    [HttpGet("sign-out")]
    public async Task<IActionResult> SignOut(CancellationToken cancellationToken)
    {
        var signOutCommand = new SignOutCommand();
        var result =
            await _distributor.PushCommand<SignOutCommand, SignOutCommandResponse>(signOutCommand, cancellationToken);
        return OkApiResult(result);
    }
}