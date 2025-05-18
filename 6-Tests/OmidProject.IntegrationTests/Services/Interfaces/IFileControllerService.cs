using System.Net.Http;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.FileContracts.Commands;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.IntegrationTests.Services.Interfaces;

public interface IFileControllerService : ITestService
{
    Task<UploadBase64DocCommandResponse> UploadBase64DocAsync(HttpClient httpClient);
}