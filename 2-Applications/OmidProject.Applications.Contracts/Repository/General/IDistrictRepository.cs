using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Domains.Domain.General;

namespace OmidProject.Applications.Contracts.Repository.General
{
    public interface IDistrictRepository
    {
        void Add(District district);
        Task AddAsync(District  district);
        void Update(District  district);
        Task UpdateAsync(District district);
        void Delete(District  district);
        Task DeleteAsync(District district);
        District GetById(int id);
        Task<District> GetByIdAsync(int id);
        List<District> GetAll();
        Task<List<District>> GetAllAsync();


    }
}
