using System.Net.Http;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.AuthenticationContracts.Commands;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.IntegrationTests.Services.Interfaces;

public interface IAuthenticationControllerService : ITestService
{
    Task<SignUpCommandResponse> SignUpAsync(SignUpCommand command, HttpClient httpClient);
    Task<SignInCommandResponse> SignInAsync(SignInCommand command, HttpClient httpClient);
}