using System.Net.Http;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.AuthenticationContracts.Commands;
using OmidProject.Frameworks.Utilities.Extensions;
using OmidProject.IntegrationTests.Configuration.Helpers;
using OmidProject.IntegrationTests.Services.Interfaces;

namespace OmidProject.IntegrationTests.Services.Implementations;

public class AuthenticationControllerService : BaseTestService, IAuthenticationControllerService
{
    /// <summary>
    ///     Asynchronously signs up a user.
    /// </summary>
    /// <param name="command">The command containing sign-up details.</param>
    /// <returns>A task that represents the asynchronous operation, containing the sign-up response.</returns>
    public async Task<SignUpCommandResponse> SignUpAsync(SignUpCommand command, HttpClient httpClient)
    {
        var response = await SendPostRequestAsync("/api/v1.0/Authentication/sign-up",
            command.CreateCommandJsonContent(), httpClient);
        return await response.DeserializeResponseAsync<SignUpCommandResponse>();
    }

    /// <summary>
    ///     Asynchronously signs in a user.
    /// </summary>
    /// <param name="command">The command containing sign-in details.</param>
    /// <returns>A task that represents the asynchronous operation, containing the sign-in response.</returns>
    public async Task<SignInCommandResponse> SignInAsync(SignInCommand command, HttpClient httpClient)
    {
        var response = await SendPostRequestAsync("/api/v1.0/Authentication/sign-in",
            command.CreateCommandJsonContent(), httpClient);
        return await response.DeserializeResponseAsync<SignInCommandResponse>();
    }
}