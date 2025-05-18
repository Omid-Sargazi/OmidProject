using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Repository.General;

public interface IProvinceRepository : IRepository
{
    void Add(Province province);
    Task AddAsync(Province province);
    void Update(Province province);
    Task UpdateAsync(Province province);
    void Delete(Province province);
    Task DeleteAsync(Province province);

    Province GetById(int id); 
    Task<Province> GetByIdAsync(int id); 
    List<Province> GetAll(); 
    Task<List<Province>> GetAllAsync();
    Task<bool> IsExistByNameAsync(string name);
}
