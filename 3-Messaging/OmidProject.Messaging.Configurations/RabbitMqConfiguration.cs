using OmidProject.Infrastructures.Settings;
using OmidProject.JobService.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OmidProject.Messaging.Configurations;

public static class RabbitMqConfiguration
{
    public static void ConfigurationRabbitMqServices(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettingKeys = configuration.GetSection("RabbitMQ");
        var rabbitMqSettings = new RabbitMQSettings()
        {
            Exchange = rabbitMqSettingKeys["Exchange"],
            Hostname = rabbitMqSettingKeys["Hostname"],
            Password = rabbitMqSettingKeys["Password"],
            QueueName = rabbitMqSettingKeys["QueueName"],
            Username = rabbitMqSettingKeys["Username"]
        };
        services.AddSingleton(rabbitMqSettings);

    }
    public static void ConfigurationRabbitMqJobServices(this IServiceCollection services)
    {
        // در صورت اجرا بودن RabbitMQ از کامنت خارج شود
        services.AddHostedService<MessageProcessor>();
    }
}