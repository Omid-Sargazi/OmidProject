using OmidProject.Applications.ACL.Contracts.Sms;
using OmidProject.Frameworks.Contracts.Common;
using OmidProject.Infrastructures.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace OmidProject.Infrastructures.ACL.Sms;

public class SmsAclService : ISmsAclService
{
    private readonly SmsSetting _smsSetting;

    public SmsAclService(IOptions<SmsSetting> smsSetting)
    {
        _smsSetting = smsSetting.Value;
    }

    public async Task<ACLResult<SmsAclOutputModel>> Send(SmsAclInputModel model)
    {
        var restClientOptions = new RestClientOptions(_smsSetting.BaseUrl)
        {
            Authenticator =
                new HttpBasicAuthenticator(_smsSetting.Username + "/" + _smsSetting.Domain, _smsSetting.Password)
        };

        var client = new RestClient(restClientOptions);

        var request = new RestRequest(_smsSetting.ResourceUrl, Method.Post);

        request.AddHeader("cache-control", "no-cache");
        request.AddHeader("accept", "application/json");

        request.AddParameter("senders", _smsSetting.Sender);
        request.AddParameter("messages", model.Message);
        request.AddParameter("recipients", model.Receiver);

        var response = await client.ExecuteAsync(request);
        var responseModel = JsonConvert.DeserializeObject<SendResponseModel>(response.Content ?? "");

        var result = new SmsAclOutputModel
        {
            IsSuccess = response.IsSuccessful && responseModel.status == _smsSetting.SuccessCode
        };

        return new ACLResult<SmsAclOutputModel>(result);
    }
}