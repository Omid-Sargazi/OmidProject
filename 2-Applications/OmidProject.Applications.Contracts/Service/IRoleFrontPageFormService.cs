using OmidProject.Applications.Contracts.FrontPageFormContracts.Queries.DTOs;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface IRoleFrontPageFormService : IService
{
    RoleFrontPageFromDto ConvertToDto(RoleFrontPageForm roleFrontPageForm);
    List<RoleFrontPageFromDto> ConvertToDto(List<RoleFrontPageForm> roleFrontPageForm);
    Task<List<RoleFrontPageForm>> ReadAllFromCache();
}