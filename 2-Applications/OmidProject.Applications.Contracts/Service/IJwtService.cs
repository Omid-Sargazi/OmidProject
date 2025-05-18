using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface IJwtService : IService
{
    string GenerateJwtToken(ApplicationUser user, IList<string> roles);
}