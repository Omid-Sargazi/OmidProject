using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Infrastructures.CommandDb.Repository;

public class AccessibleFormRepository(OmidProjectCommandDb _Db) : BaseRepository(_Db), IAccessibleFormRepository
{
    public void Add(AccessibleForm accessibleForm)
    {
        _Db.AccessibleForms.Add(accessibleForm);
        _Db.SaveChanges();
    }

    public void AddRange(List<AccessibleForm> accessibleForms)
    {
        _Db.AccessibleForms.AddRange(accessibleForms);
        _Db.SaveChanges();
    }

    public void Update(AccessibleForm accessibleForm)
    {
        _Db.AccessibleForms.Update(accessibleForm);
        _Db.SaveChanges();
    }

    public async Task<List<AccessibleForm>> GetAllAsync()
    {
        var result = await _Db.AccessibleForms.ToListAsync();
        return result;
    }

    public async Task<List<AccessibleForm>> GetByPaginationAsync(int skip, int take)
    {
        var result = await _Db.AccessibleForms
            .OrderByDescending(x => x.Id)
            .SkipTake(skip, take)
            .ToListAsync();

        return result;
    }

    public bool IsExist(int id)
    {
        return _Db.AccessibleForms.Any(x => x.Id == id);
    }

    public async Task<bool> IsExistAsync(int id, CancellationToken cancellationToken)
    {
        return await _Db.AccessibleForms.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> IsExistByRouteAsync(string route)
    {
        return await _Db.AccessibleForms.AnyAsync(x => x.Route == route);
    }

    public async Task<bool> IsExistAnotherByRouteAsync(int id, string route)
    {
        return await _Db.AccessibleForms.AnyAsync(x => x.Id != id && x.Route == route);
    }

    public async Task<AccessibleForm> GetById(int id)
    {
        var result = await _Db.AccessibleForms.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }
}