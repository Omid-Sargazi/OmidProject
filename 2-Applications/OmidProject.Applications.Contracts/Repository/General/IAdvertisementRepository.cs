using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Repository.General
{
    public interface IAdvertisementRepository : IRepository
    {
        void Add(Advertisement advertisement);
        Task AddAsync(Advertisement advertisement);
        void Update(Advertisement advertisement);
        Task UpdateAsync(Advertisement advertisement);
        void Delete(Advertisement advertisement);
        Task DeleteAsync(Advertisement advertisement);
        Advertisement GetById(int id);
        Task<Advertisement> GetByIdAsync(int id);
        List<Advertisement> GetAll();
        Task<List<Advertisement>> GetAllAsync();
    }
}
