using Microsoft.EntityFrameworkCore;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;

namespace OmidProject.Infrastructures.CommandDb.Repository;

public class ProvinceRepository(OmidProjectCommandDb _Db) : BaseRepository(_Db), IProvinceRepository
{
    public void Add(Province province)
    {
        _Db.Provinces.Add(province);
        _Db.SaveChanges();
    }

    public async Task AddAsync(Province province)
    {
        await _Db.Provinces.AddAsync(province);
        await _Db.SaveChangesAsync();
    }

    public void Update(Province province)
    {
        _Db.Provinces.Update(province);
        _Db.SaveChanges();
    }

    public async Task UpdateAsync(Province province)
    {
        _Db.Provinces.Update(province);
        await _Db.SaveChangesAsync();
    }

    public void Delete(Province province)
    {
        province.SetDelete();
        Update(province);
    }

    public async Task DeleteAsync(Province province)
    {
        province.SetDelete();
        await UpdateAsync(province);
    }

    public Province GetById(int id)
    {
        var result = _Db.Provinces.FirstOrDefault(x => x.Id == id);
        return result;
    }

    public async Task<Province> GetByIdAsync(int id)
    {
        var result = await _Db.Provinces
            .Include(x => x.Cities)
            .FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }

    public List<Province> GetAll()
    {
        var result = _Db.Provinces.ToList();
        return result;
    }

    public async Task<List<Province>> GetAllAsync()
    {
        var result = await _Db.Provinces.Include(x => x.Cities).ToListAsync();
        return result;
    }

    public async Task<bool> IsExistByNameAsync(string name)
    {
        var result = await _Db.Provinces.AnyAsync(x => x.Name == name);
        return result;
    }
}