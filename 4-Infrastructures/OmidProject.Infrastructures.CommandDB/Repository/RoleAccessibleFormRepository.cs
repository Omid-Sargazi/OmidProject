using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Infrastructures.CommandDb.Repository;

internal class RoleAccessibleFormRepository(OmidProjectCommandDb db) : BaseRepository(db), IRoleAccessibleFormRepository
{
    public void Add(RoleAccessibleForm roleAccessibleForm)
    {
        _Db.RoleAccessibleForms.Add(roleAccessibleForm);
        _Db.SaveChanges();
    }

    public async Task AddAsync(RoleAccessibleForm roleAccessibleForm, CancellationToken cancellationToken)
    {
        await _Db.RoleAccessibleForms.AddAsync(roleAccessibleForm, cancellationToken);
        await _Db.SaveChangesAsync(cancellationToken);
    }

    public void AddRange(List<RoleAccessibleForm> roleAccessibleForms)
    {
        _Db.RoleAccessibleForms.AddRange(roleAccessibleForms);
        _Db.SaveChanges();
    }

    public void Update(RoleAccessibleForm roleAccessibleForm)
    {
        _Db.RoleAccessibleForms.Update(roleAccessibleForm);
        _Db.SaveChanges();
    }

    public void UpdateRange(List<RoleAccessibleForm> roleAccessibleForms)
    {
        _Db.RoleAccessibleForms.UpdateRange(roleAccessibleForms);
        _Db.SaveChanges();
    }

    public async Task<List<RoleAccessibleForm>> GetAllAsync()
    {
        var result = await _Db.RoleAccessibleForms.Include(x => x.AccessibleForm)
            .Include(x => x.ApplicantRole)
            .ToListAsync();

        return result;
    }

    public async Task<RoleAccessibleForm> GetWithIncludes(int roleAccessibleFormId)
    {
        var result = await _Db.RoleAccessibleForms.Include(x => x.AccessibleForm)
            .Include(x => x.ApplicantRole)
            .FirstOrDefaultAsync(x => x.Id == roleAccessibleFormId);

        return result;
    }

    public async Task<List<RoleAccessibleForm>> GetWithRoleId(Guid roleId)
    {
        var result = await _Db.RoleAccessibleForms.Include(x => x.AccessibleForm)
            .Include(x => x.ApplicantRole)
            .Where(x => x.RoleId == roleId).ToListAsync();

        return result;
    }

    public async Task<List<RoleAccessibleForm>> GetWithRoleIdWithoutInclude(Guid roleId)
    {
        var result = await _Db.RoleAccessibleForms
            .Where(x => x.RoleId == roleId).ToListAsync();

        return result;
    }

    public async Task<List<RoleAccessibleForm>> GetByRoleNames(List<string> roleNames)
    {
        var result = await _Db.RoleAccessibleForms
            .Include(x => x.ApplicantRole)
            .Include(x => x.AccessibleForm)
            .Where(x => roleNames.Contains(x.ApplicantRole.Name ?? ""))
            .ToListAsync();

        return result;
    }

    public bool Exists(int accessibleFormId, Guid roleId, int? id = null)
    {
        var query = _Db.RoleAccessibleForms
            .Where(x => x.AccessibleFormId == accessibleFormId && x.RoleId == roleId);

        if (id.HasValue)
            query = query.Where(x => x.Id == id);

        var result = query.Any();

        _Db.AccessibleForms.AnyAsync(x => x.Id == id);

        return result;
    }


    public async Task<bool> ExistsAsync(int accessibleFormId, Guid roleId, CancellationToken cancellationToken, int? id = null)
    {
        var query = _Db.RoleAccessibleForms
            .Where(x => x.AccessibleFormId == accessibleFormId && x.RoleId == roleId);

        if (id.HasValue)
            query = query.Where(x => x.Id == id);

        var result = query.Any();

        await _Db.AccessibleForms.AnyAsync(x => x.Id == id, cancellationToken);

        return result;
    }
}