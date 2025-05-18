using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Repository.General
{
    public interface ICategoryRepository : IRepository
    {
        void Add(Category category);
        Task AddAsync(Category  category);
        void Update(Category  category);
        Task UpdateAsync(Category category);
        void Delete(Category  category);
        void DeleteAsync(Category  category);
        Category GetById(int id);
        Task<Category> GetByIdAsync(int id);
        List<Category> GetAll();
        Task<List<Category>> GetAllAsync();
    }
}
