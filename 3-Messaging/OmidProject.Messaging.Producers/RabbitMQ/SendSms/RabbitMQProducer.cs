using System.Text;
using System.Text.Json;
using OmidProject.Applications.ACL.Contracts.Sms;
using OmidProject.Infrastructures.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace OmidProject.Messaging.Producers.RabbitMQ.SendSms;

public class RabbitMQProducer : IMessageProducer
{
    private readonly RabbitMQSettings _settings;

    public RabbitMQProducer(IOptions<RabbitMQSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendMessageAsync(string message, string receiver)
    {
        var smsAclInputModel = new SmsAclInputModel();
        smsAclInputModel.Message = message;
        smsAclInputModel.Receiver = receiver;

        var factory = new ConnectionFactory
        {
            HostName = _settings.Hostname,
            UserName = _settings.Username,
            Password = _settings.Password
        };

        // متصل شدن به RabbitMQ به صورت Async
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            _settings.QueueName,
            true,
            false,
            false);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(smsAclInputModel));

        await channel.BasicPublishAsync(
            string.Empty,
            _settings.QueueName,
            body
        );
    }
}