using System.Linq.Expressions;
using OmidProject.Applications.Contracts.Repository;
using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Infrastructures.CommandDb.Repository;

public class GenericRepository<T, TKeyType>(OmidProjectCommandDb db)
    : BaseRepository(db), IGenericRepository<T, TKeyType>
    where T : Entity<TKeyType>
    where TKeyType : IEquatable<TKeyType>
{
    // افزودن داده جدید به دیتابیس
    public async Task<T> AddAsync(T entity)
    {
        await _Db.AddAsync(entity);
        await _Db.SaveChangesAsync();
        return entity;
    }

    // بروزرسانی موجودیت در دیتابیس
    public async Task UpdateAsync(T entity)
    {
        _Db.Update(entity);
        await _Db.SaveChangesAsync();
    }

    // حذف موجودیت از دیتابیس
    public async Task DeleteAsync(T entity)
    {
        _Db.Remove(entity);
        await _Db.SaveChangesAsync();
    }

    // بازیابی تمامی موجودیت‌ها
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var result = await _Db.Set<T>().ToListAsync();
        return result;
    }

    // بازیابی یک موجودیت بر اساس شناسه
    public async Task<T> GetAsync(TKeyType id)
    {
        var result = await _Db.Set<T>().SingleOrDefaultAsync(x => x.Id.Equals(id));
        return result;
    }

    // بررسی وجود موجودیت بر اساس شناسه
    public async Task<bool> Exist(TKeyType id)
    {
        return await _Db.Set<T>().AnyAsync(x => x.Id.Equals(id));
    }

    // متد برای دریافت با فیلتر خاص
    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _Db.Set<T>().Where(predicate).ToListAsync();
    }

    // متد برای دریافت با صفحه‌بندی
    public async Task<IReadOnlyList<T>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _Db.Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    // متد برای دریافت اولین یا پیش‌فرض
    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _Db.Set<T>().FirstOrDefaultAsync(predicate);
    }

    // متد برای شمارش تعداد کل
    public async Task<int> CountAsync()
    {
        return await _Db.Set<T>().CountAsync();
    }

    // متد برای شمارش با شرط خاص
    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _Db.Set<T>().CountAsync(predicate);
    }

    // متد برای دریافت حداکثر مقدار یک ستون خاص
    public async Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector)
    {
        return await _Db.Set<T>().MaxAsync(selector);
    }

    // متد برای دریافت حداقل مقدار یک ستون خاص
    public async Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector)
    {
        return await _Db.Set<T>().MinAsync(selector);
    }

    // متد برای انجام بروز‌رسانی گروهی
    public async Task BulkUpdateAsync(IEnumerable<T> entities)
    {
        _Db.UpdateRange(entities);
        await _Db.SaveChangesAsync();
    }

    // متد برای انجام حذف گروهی
    public async Task BulkDeleteAsync(IEnumerable<T> entities)
    {
        _Db.RemoveRange(entities);
        await _Db.SaveChangesAsync();
    }

    // جستجو با ترتیب خاص (Ascending یا Descending)
    public async Task<IReadOnlyList<T>> FindOrderedAsync(Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>> orderBy, bool ascending = true)
    {
        var query = _Db.Set<T>().Where(predicate);
        query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        return await query.ToListAsync();
    }

    // جستجو با صفحه‌بندی و ترتیب
    public async Task<IReadOnlyList<T>> GetPagedAndOrderedAsync(int pageNumber, int pageSize,
        Expression<Func<T, object>> orderBy, bool ascending = true)
    {
        var query = _Db.Set<T>().OrderBy(orderBy);
        query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    // متد برای بروزرسانی جزئی یا پچ کردن
    public async Task PatchUpdateAsync(TKeyType id, Action<T> updateAction)
    {
        var entity = await GetAsync(id);
        if (entity != null)
        {
            updateAction(entity);
            await _Db.SaveChangesAsync();
        }
    }

    // متد برای بازیابی داده‌ها با جویند‌های مختلف
    public async Task<IReadOnlyList<T>> GetWithIncludesAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _Db.Set<T>();
        foreach (var include in includes) query = query.Include(include);
        return await query.ToListAsync();
    }

    // متد برای بهینه‌سازی جستجوی سنگین با استفاده از AsNoTracking
    public async Task<IReadOnlyList<T>> GetAllAsNoTrackingAsync()
    {
        return await _Db.Set<T>().AsNoTracking().ToListAsync();
    }

    // بازیابی تنها بخشی از فیلدهای یک موجودیت با استفاده از پروجکشن
    public async Task<IReadOnlyList<TResult>> GetSelectedFieldsAsync<TResult>(Expression<Func<T, bool>> predicate,
        Expression<Func<T, TResult>> selector)
    {
        return await _Db.Set<T>().Where(predicate).Select(selector).ToListAsync();
    }

    // مدیریت تراکنش دستی
    public async Task ExecuteInTransactionAsync(Func<Task> action)
    {
        await using var transaction = await _Db.Database.BeginTransactionAsync();
        try
        {
            await action();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    // متد برای محاسبه میانگین یک ستون خاص
    public async Task<double> AverageAsync(Expression<Func<T, int>> selector)
    {
        return await _Db.Set<T>().AverageAsync(selector);
    }

    // بازیابی Top n نتایج
    public async Task<IReadOnlyList<T>> GetTopAsync(int count, Expression<Func<T, object>> orderBy,
        bool ascending = true)
    {
        var query = ascending ? _Db.Set<T>().OrderBy(orderBy) : _Db.Set<T>().OrderByDescending(orderBy);
        return await query.Take(count).ToListAsync();
    }

    // متد برای بررسی وجود داده با شرط خاص
    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _Db.Set<T>().AnyAsync(predicate,cancellationToken);
    }
}