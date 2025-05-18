using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Repository.General;

public interface IAccessibleFormRepository : IRepository
{
    void Add(AccessibleForm accessibleForm);
    void AddRange(List<AccessibleForm> accessibleForms);
    void Update(AccessibleForm accessibleForm);
    //void Delete(int id);
    Task<List<AccessibleForm>> GetAllAsync();
    Task<List<AccessibleForm>> GetByPaginationAsync(int skip, int take);
    bool IsExist(int id);
    Task<bool> IsExistByRouteAsync(string route);
    Task<bool> IsExistAnotherByRouteAsync(int id, string route);
    Task<AccessibleForm> GetById(int id);
}