using System.Threading.Tasks;
using OmidProject.Applications.Contracts.AuthenticationContracts.Commands;
using OmidProject.Host;
using OmidProject.IntegrationTests.Configuration;
using OmidProject.IntegrationTests.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace OmidProject.IntegrationTests.Scenarios;

public class RequestScenario : BaseTestClass
{
    private readonly IAuthenticationControllerService _authenticationControllerService;
    private readonly IFileControllerService _fileControllerService;

    public RequestScenario(WebApplicationFactory<Startup> factory) : base(factory)
    {
        _authenticationControllerService = ServiceProvider.GetRequiredService<IAuthenticationControllerService>();
        _fileControllerService = ServiceProvider.GetRequiredService<IFileControllerService>();
    }

    [Fact]
    public async Task Should_returnOk_AddRequest()
    {
        var client = _factory.CreateClient();

        var signUpCommand = new SignUpCommand
        {
            UserName = "testuser",
            FirstName = "testuser",
            LastName = "testuser",
            Email = "testuser@gmail.com",
            Password = "Test@123"
        };

        var signUp = await _authenticationControllerService.SignUpAsync(signUpCommand, client);

        var signInCommand = new SignInCommand
        {
            UserName = signUpCommand.UserName,
            Password = signUpCommand.Password,
            IsPersistent = true
        };

        var signIn = await _authenticationControllerService.SignInAsync(signInCommand, client);

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {signIn.Token}");
    }
}