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
    public class CityRepository(OmidProjectCommandDb _Db) :BaseRepository(_Db), ICityRepository
    {
        public void Add(City city)
        {
            _Db.Cities.Add(city);
            _Db.SaveChanges();
        }

        public async Task AddAsync(City city)
        {
            await _Db.Cities.AddAsync(city);
            await _Db.SaveChangesAsync();
        }

        public void Update(City city)
        {
            _Db.Cities.Update(city);
            _Db.SaveChanges();
        }

        public async Task UpdateAsync(City city)
        {
            _Db.Cities.Update(city);
           await _Db.SaveChangesAsync();
        }

        public void Delete(City city)
        {
            city.SetDelete();
            Update(city);
        }

        public async Task DeleteAsync(City city)
        {
            city.SetDelete();
            await UpdateAsync(city);
        }

        public City GetById(int id)
        {
            var result = _Db.Cities.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public async Task<City> GetByIdAsync(int id)
        {
            var result = await _Db.Cities.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public List<City> GetAll()
        {
            var result = _Db.Cities.ToList();
            return result;
        }

        public List<City> GetAll(int? provinceId)
        {
            var query = _Db.Cities.AsQueryable();

            if (provinceId.HasValue)
                query = query.Where(x => x.ProvinceId == provinceId.Value);

            var result = query.ToList();
            return result;         
        }

        public async Task<List<City>> GetAllAsync()
        {
            var result = await _Db.Cities.ToListAsync();
            return result;
        }

        public List<City> GetAllByProvince(int provinceId)
        {
            var result = _Db.Cities.Where(x => x.ProvinceId == provinceId).ToList();
            return result;
        }
    }
}
