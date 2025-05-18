using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.AuthenticationContracts.Commands;

/// <summary>
///     Represents a command to sign up a new user.
/// </summary>
public class SignUpCommand : Command
{
    /// <summary>
    ///     Gets or sets the username for the new user.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     Gets or sets the password for the new user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    ///     Gets or sets the first name of the new user.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///     Gets or sets the last name of the new user.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    ///     Gets or sets the email address of the new user.
    /// </summary>
    public string Email { get; set; }
}

/// <summary>
///     Represents the response received after attempting to sign up a new user.
/// </summary>
public class SignUpCommandResponse : CommandResponse
{
    /// <summary>
    ///     Gets or sets the message indicating the result of the sign-up attempt.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    ///     Gets or sets a prefix associated with the response.
    /// </summary>
    public string Prefix { get; set; }

    /// <summary>
    ///     Gets or sets the status code of the sign-up attempt.
    /// </summary>
    public int StatusCode { get; set; }
}