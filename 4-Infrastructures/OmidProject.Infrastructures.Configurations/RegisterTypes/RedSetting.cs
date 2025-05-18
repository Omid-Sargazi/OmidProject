using OmidProject.Infrastructures.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OmidProject.Infrastructures.Configurations.RegisterTypes;

public static class RedSetting
{
    public static void AddSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<JwtSetting>(options => configuration.GetSection("JwtSetting").Bind(options));
        serviceCollection.Configure<GeneralSettings>(options =>
            configuration.GetSection("GeneralSettings").Bind(options));
        serviceCollection.Configure<SmsSetting>(options => configuration.GetSection("Sms").Bind(options));
        serviceCollection.Configure<ProjectPathSettings>(options => configuration.GetSection("ProjectPathSettings").Bind(options));
        serviceCollection.Configure<RabbitMQSettings>(options => configuration.GetSection("RabbitMQ").Bind(options));
    }
}