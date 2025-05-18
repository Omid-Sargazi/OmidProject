using OmidProject.Applications.Contracts.AccessibleFormContracts.Queries.DTOs;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface IAccessibleFormService : IService
{
    AccessibleFormDto ConvertToDto(AccessibleForm agreement);
    List<AccessibleFormDto> ConvertToDto(List<AccessibleForm> agreements);
}