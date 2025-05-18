namespace OmidProject.Frameworks.Utilities.Extensions;

public static class GuidExtension
{
    public static bool IsEmpty(this Guid value)
    {
        return value == Guid.Empty;
    }

    public static bool IsNullOrEmpty(this Guid? value)
    {
        return value == null || value == Guid.Empty;
    }
}