using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Repository.General;

public interface ICityRepository : IRepository
{
    void Add(Domains.Domain.General.City city);
    Task AddAsync(Domains.Domain.General.City city);
    void Update(Domains.Domain.General.City city);
    Task UpdateAsync(Domains.Domain.General.City city);
    void Delete(Domains.Domain.General.City city);
    Task DeleteAsync(Domains.Domain.General.City city);

    Domains.Domain.General.City GetById(int id); 
    Task<Domains.Domain.General.City> GetByIdAsync(int id); 
    List<Domains.Domain.General.City> GetAll(); 
    Task<List<Domains.Domain.General.City>> GetAllAsync();

    List<Domains.Domain.General.City> GetAllByProvince(int provinceId);

}