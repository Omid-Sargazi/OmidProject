using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Infrastructures.CommandDb.Repository
{
    public class AdvertisementRepository(OmidProjectCommandDb _Db) :BaseRepository(_Db), IAdvertisementRepository
    {
        public void Add(Advertisement advertisement)
        {
            _Db.Advertisements.Add(advertisement);
            _Db.SaveChanges();
        }

        public async Task AddAsync(Advertisement advertisement)
        {
           await _Db.Advertisements.AddAsync(advertisement);
           await _Db.SaveChangesAsync();
        }

        public void Update(Advertisement advertisement)
        {
            _Db.Advertisements.Update(advertisement);
            _Db.SaveChanges();
        }

        public async Task UpdateAsync(Advertisement advertisement)
        {
            _Db.Advertisements.Update(advertisement);
            await _Db.SaveChangesAsync();
        }

        public void Delete(Advertisement advertisement)
        {
           advertisement.SetDelete();
           Update(advertisement);
        }

        public async Task DeleteAsync(Advertisement advertisement)
        {
            advertisement.SetDelete();
            UpdateAsync(advertisement);
        }

        public Advertisement GetById(int id)
        {
            var result = _Db.Advertisements.FirstOrDefault(a => a.Id == id);
            return result;
        }

        public async Task<Advertisement> GetByIdAsync(int id)
        {
            var result = await _Db.Advertisements.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public List<Advertisement> GetAll()
        {
            var result = _Db.Advertisements.ToList();
            return result;
        }

        public async Task<List<Advertisement>> GetAllAsync()
        {
            var result = await _Db.Advertisements.ToListAsync();
            return result;
        }
    }
}
