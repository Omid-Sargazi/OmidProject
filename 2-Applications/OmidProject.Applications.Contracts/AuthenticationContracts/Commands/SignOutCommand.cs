using System.Security.Claims;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.AuthenticationContracts.Commands;

/// <summary>
///     Command for signing out a user.
/// </summary>
public class SignOutCommand : Command
{
    /// <summary>
    ///     Gets or sets the ClaimsPrincipal of the user to sign out.
    /// </summary>
    public ClaimsPrincipal User { get; set; }
}

/// <summary>
///     Response class for the SignOutCommand.
/// </summary>
public class SignOutCommandResponse : CommandResponse
{
    /// <summary>
    ///     Gets or sets the message indicating the result of the sign-out operation.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    ///     Gets or sets the prefix associated with the response message.
    /// </summary>
    public string Prefix { get; set; }

    /// <summary>
    ///     Gets or sets the response code indicating the status of the sign-out operation.
    /// </summary>
    public int Code { get; set; }
}