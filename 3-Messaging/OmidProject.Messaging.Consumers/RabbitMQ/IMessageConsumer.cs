using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Messaging.Consumers.RabbitMQ;

public interface IMessageConsumer : IRabbitMq
{
    Task StartConsuming(CancellationToken stoppingToken);
}