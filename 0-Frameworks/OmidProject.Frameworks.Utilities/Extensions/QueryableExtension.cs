namespace OmidProject.Frameworks.Utilities.Extensions;

public static class QueryableExtension
{
    public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize)
    {
        var result = new PagedResult<T>();
        result.CurrentPage = page;
        result.PageSize = pageSize;
        result.RowCount = query.Count();

        var pageCount = (double) result.RowCount / pageSize;
        result.PageCount = (int) Math.Ceiling(pageCount);

        var skip = (page - 1) * pageSize;
        result.Results = query.Skip(skip).Take(pageSize).ToList();

        return result;
    }

    public static IQueryable<T> SkipTake<T>(this IOrderedQueryable<T> query, int skip, int take)
    {
        var result = query.Skip(skip).Take(take);

        return result;
    }
}

public class PagedResult<T>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int RowCount { get; set; }
    public int PageCount { get; set; }
    public List<T> Results { get; set; }
}