using System.Net.Http;
using System.Threading.Tasks;

namespace OmidProject.IntegrationTests.Services;

public class BaseTestService
{
    /// <summary>
    ///     Sends a POST request to the specified URI with the given content.
    /// </summary>
    public async Task<HttpResponseMessage> SendPostRequestAsync(string uri, HttpContent content, HttpClient httpClient)
    {
        var response = await httpClient.PostAsync(uri, content);
        response.EnsureSuccessStatusCode();
        return response;
    }

    /// <summary>
    ///     Sends a GET request to the specified URI.
    /// </summary>
    public async Task<HttpResponseMessage> SendGetRequestAsync(string uri, HttpClient httpClient)
    {
        var response = await httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        return response;
    }
}