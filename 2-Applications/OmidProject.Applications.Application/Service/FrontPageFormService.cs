using OmidProject.Applications.Contracts.FrontPageFormContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.Service;

public class FrontPageFormService : IFrontPageFormService
{
    public FrontPageFromDto ConvertToDto(FrontPageForm frontPageFrom)
    {
        var result = frontPageFrom.MapTo<FrontPageFromDto>();

        return result;
    }

    public List<FrontPageFromDto> ConvertToDto(List<FrontPageForm> frontPageFrom)
    {
        var result = frontPageFrom.Select(ConvertToDto).ToList();
        return result;
    }
}