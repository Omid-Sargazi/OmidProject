using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OmidProject.Host;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace OmidProject.IntegrationTests;

public class HealthCheckTests : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly HttpClient _httpClient;

    public HealthCheckTests(WebApplicationFactory<Startup> factory)
    {
        _httpClient = factory.CreateDefaultClient();
    }

    [Fact]
    public async Task Should_returnOk_HealthCheck()
    {
        var response = await _httpClient.GetAsync("/healthcheck");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}