using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Repository.General;

public interface IRoleFrontPageFormRepository : IRepository
{
    void Add(RoleFrontPageForm roleFrontPageForm);
    void AddRange(List<RoleFrontPageForm> roleFrontPageForm);
    void Update(RoleFrontPageForm roleFrontPageForm);

    void UpdateRange(List<RoleFrontPageForm> roleFrontPageForms);

    //void Delete(int id);
    Task<RoleFrontPageForm> GetWithIncludes(int roleFrontPageForm);
    Task<List<RoleFrontPageForm>> GetWithRoleIdWithoutInclude(Guid roleId);
    Task<List<RoleFrontPageForm>> GetAllAsync();
    bool Exists(int accessibleFormId, Guid roleId, int? id = null);
    Task<List<RoleFrontPageForm>> GetWithRoleId(Guid roleId);
    Task<List<RoleFrontPageForm>> GetByRoleNames(List<string> roleNames);
}