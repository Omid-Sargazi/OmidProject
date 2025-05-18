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
    public class AdvertisementImageRepository(OmidProjectCommandDb _Db) :BaseRepository(_Db), IAdvertisementImageRepository
    {
        public void Add(AdvertisementImage advertisementImage)
        {
            _Db.AdvertisementImages.Add(advertisementImage);
            _Db.SaveChanges();
        }

        public async Task AddAsync(AdvertisementImage advertisementImage)
        {
            await _Db.AdvertisementImages.AddAsync(advertisementImage);
            await _Db.SaveChangesAsync();
        }

        public void Update(AdvertisementImage advertisementImage)
        {
            _Db.AdvertisementImages.Update(advertisementImage);
            _Db.SaveChanges();
        }

        public async Task UpdateAsync(AdvertisementImage advertisementImage)
        {
            _Db.AdvertisementImages.Update(advertisementImage);
            await _Db.SaveChangesAsync();
        }

        public void Delete(AdvertisementImage advertisementImage)
        {
            advertisementImage.SetDelete();
            Update(advertisementImage);
        }

        public void DeleteAsync(AdvertisementImage advertisementImage)
        {
            advertisementImage.SetDelete();
            UpdateAsync(advertisementImage);
        }

        public AdvertisementImage GetById(int id)
        {
           var result = _Db.AdvertisementImages.FirstOrDefault(x => x.Id==id);
           return result;
        }

        public async Task<AdvertisementImage> GetByIdAsync(int id)
        {
            var result = await _Db.AdvertisementImages.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public List<AdvertisementImage> GetAll()
        {
            var result = _Db.AdvertisementImages.ToList();
            return result;
        }

        public async Task<List<AdvertisementImage>> GetAllAsync()
        {
            var result = await _Db.AdvertisementImages.ToListAsync();
            return result;  
        }
    }
}
