using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace OmidProject.Host.Middleware;

public class CustomSerilogMiddleware
{
    private readonly ILogger<CustomSerilogMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomSerilogMiddleware(RequestDelegate next, ILogger<CustomSerilogMiddleware> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _next = next;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Invoke(HttpContext context)
    {
        // If a file exists, skip logging
        if (!RequestHasFile(context.Request) && !RequestIsFromFileController(context.Request))
        {
            // First, get the incoming request
            var requestLogModel = await FormatRequest(context.Request);

            // Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            // Create a new memory stream...
            using var responseBody = new MemoryStream();
            // ...and use that for the temporary response body
            context.Response.Body = responseBody;

            // Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);

            //Update requestLogModel by claims
            await UpdateRequestModelByClaims(requestLogModel);

            // Format the response from the server
            var responseLogModel = await FormatResponse(context.Response);

            switch (responseLogModel.StatusCode)
            {
                case (int) HttpStatusCode.OK:
                    _logger.LogInformation("{HttpMethod} {Request} {Response} {IsCustomLog}", requestLogModel.Method,
                        requestLogModel.ToString(),
                        responseLogModel.ToString(), true);
                    break;
                case (int) HttpStatusCode.Forbidden:
                case (int) HttpStatusCode.Conflict:
                    _logger.LogWarning("{HttpMethod} {Request} {Response} {IsCustomLog}", requestLogModel.Method,
                        requestLogModel.ToString(),
                        responseLogModel.ToString(), true);
                    break;
                case (int) HttpStatusCode.BadRequest:
                case (int) HttpStatusCode.InternalServerError:
                    _logger.LogError("{HttpMethod} {Request} {Response} {IsCustomLog}", requestLogModel.Method,
                        requestLogModel.ToString(),
                        responseLogModel.ToString(), true);
                    break;
            }

            // Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
            await responseBody.CopyToAsync(originalBodyStream);
        }
        else
        {
            // If a file exists, you might want to handle it differently or not log at all.
            await _next(context);
        }
    }

    private async Task<RequestLogModel> FormatRequest(HttpRequest request)
    {
        var apiAddress = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
        var headersAsJson = JsonConvert.SerializeObject(request.Headers, Formatting.None);

        // This line allows us to set the reader for the request back at the beginning of its stream.
        request.EnableBuffering();

        // We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];

        // ...Then we copy the entire request stream into the new buffer.
        await request.Body.ReadAsync(buffer, 0, buffer.Length);

        // We convert the byte[] into a string using UTF8 encoding...
        var bodyAsText = Encoding.UTF8.GetString(buffer);

        // ..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
        //request.Body = body;

        request.Body.Position = 0; //rewinding the stream to 0

        return new RequestLogModel(apiAddress, request.Method, bodyAsText, headersAsJson);
    }

    private async Task UpdateRequestModelByClaims(RequestLogModel requestLogModel)
    {
        //read Username and UserId from Claims
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var nameClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
        var identifierClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        var username = nameClaim?.Value ?? "";
        var userId = identifierClaim?.Value ?? "";

        requestLogModel.Update(username, userId);
    }

    private async Task<ResponseLogModel> FormatResponse(HttpResponse response)
    {
        // We need to read the response stream from the beginning...
        response.Body.Seek(0, SeekOrigin.Begin);

        // ...and copy it into a string
        var text = await new StreamReader(response.Body).ReadToEndAsync();

        // We need to reset the reader for the response so that the client can read it.
        response.Body.Seek(0, SeekOrigin.Begin);

        // Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
        return new ResponseLogModel(response.StatusCode, text);
    }

    private bool RequestHasFile(HttpRequest request)
    {
        var hasFile = false;
        try
        {
            hasFile = request.Form.Files?.Any() ?? false;
        }
        catch (Exception e)
        {
        }

        return hasFile;
    }

    private bool RequestIsFromFileController(HttpRequest request)
    {
        return request.Path.HasValue && request.Path.Value.ToLower().Contains("/file/");
    }

    private class ResponseLogModel
    {
        public ResponseLogModel(int statusCode, string bodyAsJson)
        {
            StatusCode = statusCode;
            BodyAsJson = bodyAsJson;
        }

        public int StatusCode { get; }
        public string BodyAsJson { get; }

        public override string ToString()
        {
            return $"ResponseStatusCode: {StatusCode}\r\nResponseBody: {BodyAsJson}\r\n";
        }
    }

    private class RequestLogModel
    {
        public RequestLogModel(string url, string method, string bodyAsJson, string headersAsJson)
        {
            Url = url;
            Method = method;
            BodyAsJson = bodyAsJson;
            HeadersAsJson = headersAsJson;
        }

        public string Url { get; }
        public string Method { get; }
        public string BodyAsJson { get; }
        public string HeadersAsJson { get; }
        public string Username { get; set; }
        public string UserId { get; set; }

        public void Update(string username, string userId)
        {
            Username = username;
            UserId = userId;
        }

        public override string ToString()
        {
            return
                $"RequestUrl: {Url}\r\nUsername: {Username}\r\nUserId: {UserId}\r\nRequestHeaders: {HeadersAsJson}\r\nRequestBody: {BodyAsJson}\r\n";
        }
    }
}