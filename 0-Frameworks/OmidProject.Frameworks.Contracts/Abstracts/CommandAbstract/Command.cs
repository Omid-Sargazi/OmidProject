using Newtonsoft.Json;

namespace OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

public abstract class Command
{
    public Command()
    {
        Metadata = new Metadata();
    }

    [JsonIgnore] public Metadata Metadata { get; set; }
}