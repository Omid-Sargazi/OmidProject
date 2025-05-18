using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Messaging.Producers.RabbitMQ;

public interface IMessageProducer : IRabbitMq
{
    Task SendMessageAsync(string message, string receiver);
}