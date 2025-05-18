namespace OmidProject.Infrastructures.Settings;

public class RabbitMQSettings
{
    public string Hostname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Exchange { get; set; }
    public string QueueName { get; set; }
}