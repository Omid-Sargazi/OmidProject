using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.AuthenticationContracts.Commands;

/// <summary>
///     Represents a command for user sign-in.
/// </summary>
public class SignInCommand : Command
{
    /// <summary>
    ///     Gets or sets the username of the user attempting to sign in.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     Gets or sets the password of the user attempting to sign in.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the user wants to stay signed in.
    /// </summary>
    public bool IsPersistent { get; set; }
}

/// <summary>
///     Represents the response returned after a user sign-in attempt.
/// </summary>
public class SignInCommandResponse : CommandResponse
{
    /// <summary>
    ///     Gets or sets the authentication token for the signed-in user.
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    ///     Gets or sets the refresh token for obtaining new tokens.
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    ///     Gets or sets the expiration time of the token.
    /// </summary>
    public string ExpiredAt { get; set; }
}