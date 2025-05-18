using OmidProject.Messaging.Consumers.RabbitMQ;
using Microsoft.Extensions.Hosting;

namespace OmidProject.JobService.RabbitMQ;

public class MessageProcessor(IMessageConsumer consumer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // شروع مصرف پیام‌ها
        //await consumer.StartConsuming(stoppingToken);
    }
}