using OmidProject.Applications.Contracts.UserManagerContracts.Queries.DTOs;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface IApplicantUserService : IService
{
    ApplicationUserDto ConvertToDto(ApplicationUser applicantUser);
    List<ApplicationUserDto> ConvertToDto(List<ApplicationUser> applicantUsers);
}