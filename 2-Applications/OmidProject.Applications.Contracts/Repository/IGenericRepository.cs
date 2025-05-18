using System.Linq.Expressions;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Repository;

public interface IGenericRepository<T, in TKeyType> : IRepository
{
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> GetAsync(TKeyType id);
    Task<bool> Exist(TKeyType id);
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<IReadOnlyList<T>> GetPagedAsync(int pageNumber, int pageSize);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector);
    Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector);
    Task BulkUpdateAsync(IEnumerable<T> entities);
    Task BulkDeleteAsync(IEnumerable<T> entities);

    Task<IReadOnlyList<T>> FindOrderedAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy,
        bool ascending = true);

    Task<IReadOnlyList<T>> GetPagedAndOrderedAsync(int pageNumber, int pageSize, Expression<Func<T, object>> orderBy,
        bool ascending = true);

    Task PatchUpdateAsync(TKeyType id, Action<T> updateAction);
    Task<IReadOnlyList<T>> GetWithIncludesAsync(params Expression<Func<T, object>>[] includes);
    Task<IReadOnlyList<T>> GetAllAsNoTrackingAsync();

    Task<IReadOnlyList<TResult>> GetSelectedFieldsAsync<TResult>(Expression<Func<T, bool>> predicate,
        Expression<Func<T, TResult>> selector);

    Task ExecuteInTransactionAsync(Func<Task> action);
    Task<double> AverageAsync(Expression<Func<T, int>> selector);
    Task<IReadOnlyList<T>> GetTopAsync(int count, Expression<Func<T, object>> orderBy, bool ascending = true);
}