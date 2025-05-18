using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;

namespace OmidProject.Infrastructures.CommandDb.Repository
{
    public class DistrictRepository(OmidProjectCommandDb _Db) :BaseRepository(_Db), IDistrictRepository
    {
        public void Add(District district)
        {
            _Db.Districts.Add(district);
            _Db.SaveChanges();
        }

        public async Task AddAsync(District district)
        {
            await _Db.Districts.AddAsync(district);
            await _Db.SaveChangesAsync();
        }

        public void Update(District district)
        {
            _Db.Districts.Update(district);
            _Db.SaveChanges();
        }

        public async Task UpdateAsync(District district)
        {
            _Db.Districts.Update(district);
            await _Db.SaveChangesAsync();
        }

        public void Delete(District district)
        {
            district.SetDelete();
            Update(district);
        }

        public async Task DeleteAsync(District district)
        {
            district.SetDelete();
            UpdateAsync(district);
        }

        public District GetById(int id)
        {
            var result = _Db.Districts.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public async Task<District> GetByIdAsync(int id)
        {
            var result = await _Db.Districts.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public List<District> GetAll()
        {
            var result = _Db.Districts.ToList();
            return result;
        }

        public async Task<List<District>> GetAllAsync()
        {
            var result = await _Db.Districts.ToListAsync();
            return result;
        }
    }
}
