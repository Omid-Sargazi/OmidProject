using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Domains.Domain.General;

namespace OmidProject.Applications.Contracts.Repository.General
{
    public interface IAdvertisementImageRepository
    {
        void Add(AdvertisementImage  advertisementImage);
        Task AddAsync(AdvertisementImage advertisementImage);
        void Update(AdvertisementImage advertisementImage);
        Task UpdateAsync(AdvertisementImage advertisementImage);
        void Delete(AdvertisementImage advertisementImage);
        void DeleteAsync(AdvertisementImage advertisementImage);
        AdvertisementImage GetById(int id);
        Task<AdvertisementImage> GetByIdAsync(int id);
        List<AdvertisementImage> GetAll();
        Task<List<AdvertisementImage>> GetAllAsync();

    }
}
