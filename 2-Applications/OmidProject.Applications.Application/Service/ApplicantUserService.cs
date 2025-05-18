using OmidProject.Applications.Contracts.Service;
using OmidProject.Applications.Contracts.UserManagerContracts.Queries.DTOs;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.Service;

public class ApplicantUserService : IApplicantUserService
{
    public ApplicationUserDto ConvertToDto(ApplicationUser applicantUser)
    {
        var result = applicantUser.MapTo<ApplicationUserDto>();
        result.FullName = applicantUser.GetFullName();
        return result;
    }

    public List<ApplicationUserDto> ConvertToDto(List<ApplicationUser> applicantUsers)
    {
        var result = applicantUsers.Select(ConvertToDto).ToList();
        return result;
    }
}