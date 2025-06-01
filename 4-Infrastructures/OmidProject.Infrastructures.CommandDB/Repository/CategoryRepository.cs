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
    public class CategoryRepository(OmidProjectCommandDb _Db) :BaseRepository(_Db), ICategoryRepository
    {
        public void Add(Category category)
        {
            _Db.Categories.Add(category);
            _Db.SaveChanges();
        }

        public async Task AddAsync(Category category)
        {
            await _DbBase.Categories.AddAsync(category);
            await _Db.SaveChangesAsync();
        }

        public void Update(Category category)
        {
            _Db.Categories.Update(category);
            _Db.SaveChanges();
        }

        public async Task UpdateAsync(Category category)
        {
            _Db.Categories.Update(category);
            await _Db.SaveChangesAsync();   
        }

        public void Delete(Category category)
        {
            category.SetDelete();
            Update(category);

        }

        public void DeleteAsync(Category category)
        {
            category.SetDelete();
        }

        public Category GetById(int id)
        {
            var result = _Db.Categories.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var result = await _Db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public List<Category> GetAll()
        {
            var result = _Db.Categories.ToList();
            return result;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var result = await _Db.Categories
                .Include(x=>x.Parent)
                .Include(x=>x.Child)
                .ToListAsync();
            return result;
        }
    }
}
