using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Infrastructures.CommandDb.Repository;

public class FrontPageFormRepository : BaseRepository, IFrontPageFormRepository
{
    public FrontPageFormRepository(OmidProjectCommandDb db) : base(db)
    {
    }

    public void Add(FrontPageForm frontPageForm)
    {
        _Db.FrontPageForms.Add(frontPageForm);
        _Db.SaveChanges();
    }

    public void AddRange(List<FrontPageForm> frontPageForm)
    {
        _Db.FrontPageForms.AddRange(frontPageForm);
        _Db.SaveChanges();
    }

    public void Update(FrontPageForm frontPageForm)
    {
        _Db.FrontPageForms.Update(frontPageForm);
        _Db.SaveChanges();
    }

    public async Task<List<FrontPageForm>> GetAllAsync()
    {
        var result = await _Db.FrontPageForms.ToListAsync();
        return result;
    }

    public async Task<List<FrontPageForm>> GetByPaginationAsync(int skip, int take)
    {
        var result = await _Db.FrontPageForms
            .OrderByDescending(x => x.Id)
            .SkipTake(skip, take)
            .ToListAsync();

        return result;
    }

    public bool IsExist(int id)
    {
        return _Db.FrontPageForms.Any(x => x.Id == id);
    }

    public async Task<bool> IsExistByRouteAsync(string route)
    {
        return await _Db.FrontPageForms.AnyAsync(x => x.Route == route);
    }

    public async Task<bool> IsExistAnotherByRouteAsync(int id, string route)
    {
        return await _Db.FrontPageForms.AnyAsync(x => x.Id != id && x.Route == route);
    }

    public async Task<FrontPageForm> GetById(int id)
    {
        var result = await _Db.FrontPageForms.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }
}