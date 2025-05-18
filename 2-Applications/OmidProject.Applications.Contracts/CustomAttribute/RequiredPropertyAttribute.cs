namespace OmidProject.Applications.Contracts.CustomAttribute;

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
public class RequiredPropertyAttribute : Attribute
{
    public RequiredPropertyAttribute(string displayName)
    {
        DisplayName = displayName;
    }

    public string DisplayName { get; }
}