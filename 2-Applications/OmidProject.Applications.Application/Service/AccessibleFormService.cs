using OmidProject.Applications.Contracts.AccessibleFormContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.Service;

public class AccessibleFormService : IAccessibleFormService
{
    public AccessibleFormDto ConvertToDto(AccessibleForm accessibleForm)
    {
        //var result = new AccessibleFormDto
        //{
        //    Id = accessibleForm.Id,
        //    Title = accessibleForm.Title,
        //    Route = accessibleForm.Route
        //};

        var result = accessibleForm.MapTo<AccessibleFormDto>();

        return result;
    }

    public List<AccessibleFormDto> ConvertToDto(List<AccessibleForm> accessibleForm)
    {
        var result = accessibleForm.Select(ConvertToDto).ToList();
        return result;
    }
}