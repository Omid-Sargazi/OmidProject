using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Repository.General;

public interface IRoleAccessibleFormRepository : IRepository
{
    void Add(RoleAccessibleForm roleAccessibleForm);
    Task AddAsync(RoleAccessibleForm roleAccessibleForm, CancellationToken cancellationToken);
    void AddRange(List<RoleAccessibleForm> roleAccessibleForms);
    void Update(RoleAccessibleForm roleAccessibleForm);
    void UpdateRange(List<RoleAccessibleForm> roleAccessibleForms);
    //void Delete(int id);
    Task<RoleAccessibleForm> GetWithIncludes(int roleAccessibleFormId);
    Task<List<RoleAccessibleForm>> GetAllAsync();
    bool Exists(int accessibleFormId, Guid roleId, int? id = null);
    Task<bool> ExistsAsync(int accessibleFormId, Guid roleId, CancellationToken cancellationToken, int? id = null);
    Task<List<RoleAccessibleForm>> GetWithRoleId(Guid roleId);
    Task<List<RoleAccessibleForm>> GetWithRoleIdWithoutInclude(Guid roleId);
    Task<List<RoleAccessibleForm>> GetByRoleNames(List<string> roleNames);
}