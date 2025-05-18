namespace OmidProject.Frameworks.Contracts.Abstracts;

public sealed class Metadata
{
    public Metadata()
    {
        RoleNames ??= new List<string>();
    }

    public Guid? UserId { get; set; }
    public string? UserName { get; set; }
    public List<string> RoleNames { get; set; }
}