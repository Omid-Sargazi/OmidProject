using OmidProject.Applications.Contracts.FrontPageFormContracts.Queries.DTOs;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface IFrontPageFormService : IService
{
    FrontPageFromDto ConvertToDto(FrontPageForm frontPageFrom);
    List<FrontPageFromDto> ConvertToDto(List<FrontPageForm> frontPageFrom);
}