using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Infrastructures.CommandDb.Repository;

public class RoleFrontPageFormRepository : BaseRepository, IRoleFrontPageFormRepository
{
    public RoleFrontPageFormRepository(OmidProjectCommandDb db) : base(db)
    {
    }

    public void Add(RoleFrontPageForm roleFrontPageForm)
    {
        _Db.RoleFrontPageForms.Add(roleFrontPageForm);
        _Db.SaveChanges();
    }

    public void AddRange(List<RoleFrontPageForm> roleFrontPageForm)
    {
        _Db.RoleFrontPageForms.AddRange(roleFrontPageForm);
        _Db.SaveChanges();
    }

    public void Update(RoleFrontPageForm roleFrontPageForm)
    {
        _Db.RoleFrontPageForms.Update(roleFrontPageForm);
        _Db.SaveChanges();
    }

    public void UpdateRange(List<RoleFrontPageForm> roleFrontPageForms)
    {
        _Db.RoleFrontPageForms.UpdateRange(roleFrontPageForms);
        _Db.SaveChanges();
    }


    public async Task<RoleFrontPageForm> GetWithIncludes(int roleFrontPageForm)
    {
        var result = await _Db.RoleFrontPageForms.Include(x => x.FrontPageForm)
            .Include(x => x.ApplicantRole)
            .FirstOrDefaultAsync(x => x.Id == roleFrontPageForm);

        return result;
    }

    public async Task<List<RoleFrontPageForm>> GetAllAsync()
    {
        var result = await _Db.RoleFrontPageForms.Include(x => x.FrontPageForm)
            .Include(x => x.ApplicantRole)
            .ToListAsync();

        return result;
    }

    public async Task<List<RoleFrontPageForm>> GetWithRoleId(Guid roleId)
    {
        var result = await _Db.RoleFrontPageForms.Include(x => x.FrontPageForm)
            .Include(x => x.ApplicantRole)
            .Where(x => x.RoleId == roleId).ToListAsync();

        return result;
    }

    public async Task<List<RoleFrontPageForm>> GetByRoleNames(List<string> roleNames)
    {
        var result = await _Db.RoleFrontPageForms
            .Include(x => x.ApplicantRole)
            .Include(x => x.FrontPageForm)
            .Where(w => roleNames.Contains(w.ApplicantRole.Name ?? ""))
            .ToListAsync();

        return result;
    }

    public async Task<List<RoleFrontPageForm>> GetWithRoleIdWithoutInclude(Guid roleId)
    {
        var result = await _Db.RoleFrontPageForms
            .Where(x => x.RoleId == roleId).ToListAsync();

        return result;
    }

    public bool Exists(int frontPageFormId, Guid roleId, int? id = null)
    {
        var query = _Db.RoleFrontPageForms
            .Where(x => x.FrontPageFormId == frontPageFormId && x.RoleId == roleId);

        if (id.HasValue)
            query = query.Where(x => x.Id == id);

        var result = query.Any();

        return result;
    }
}