using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Repository.Identity;

public interface IApplicationRoleRepository : IRepository
{
    Task<ApplicationRole> FindByNameAsync(string roleName);
    Task<ApplicationRole> FindByIdAsync(string id);
    Task CreateAsync(ApplicationRole applicationRole);
}