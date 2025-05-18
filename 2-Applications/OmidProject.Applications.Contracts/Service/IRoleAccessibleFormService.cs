using OmidProject.Applications.Contracts.AccessibleFormContracts.Queries.DTOs;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface IRoleAccessibleFormService : IService
{
    RoleAccessibleFormDto ConvertToDto(RoleAccessibleForm roleAccessibleForm);
    List<RoleAccessibleFormDto> ConvertToDto(List<RoleAccessibleForm> roleAccessibleForm);
    Task<List<RoleAccessibleForm>> ReadAllFromCache();
    Task ResetCache();
}