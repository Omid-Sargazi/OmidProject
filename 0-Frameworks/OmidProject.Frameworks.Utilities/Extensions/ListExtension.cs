using System.Collections;

namespace OmidProject.Frameworks.Utilities.Extensions;

public static class ListExtension
{
    public static bool IsNotNullAndAny<T>(this IList<T> value)
    {
        return value == null && value.Any();
    }

    public static bool IsNullOrEmpty(this IList value)
    {
        return value == null || value.Count == 0;
    }

    public static bool IsNull(this IList value)
    {
        return value == null;
    }

    public static bool IsEmpty(this IList value)
    {
        return value != null && value.Count == 0;
    }

    public static bool HasDuplicate<T>(this IList<T> list)
    {
        return list.GroupBy(x => x)
            .Any(g => g.Count() > 1);
    }
}