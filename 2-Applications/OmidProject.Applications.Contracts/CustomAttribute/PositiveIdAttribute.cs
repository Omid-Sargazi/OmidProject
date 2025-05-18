namespace OmidProject.Applications.Contracts.CustomAttribute;

[AttributeUsage(AttributeTargets.Property)]
public class PositiveIdAttribute : Attribute
{
    public PositiveIdAttribute(string displayName)
    {
        DisplayName = displayName;
    }

    public string DisplayName { get; }
}