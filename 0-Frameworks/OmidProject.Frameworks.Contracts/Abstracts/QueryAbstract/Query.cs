using Newtonsoft.Json;

namespace OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

public abstract class Query
{
    protected Query()
    {
        Metadata = new Metadata();
    }

    [JsonIgnore] public Metadata Metadata { get; set; }
}