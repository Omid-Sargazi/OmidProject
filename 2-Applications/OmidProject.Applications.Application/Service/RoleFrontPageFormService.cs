using OmidProject.Applications.Contracts.FrontPageFormContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Utilities.Extensions;
using OmidProject.Infrastructures.Settings;
using Microsoft.Extensions.Options;

namespace OmidProject.Applications.Application.Service;

public class RoleFrontPageFormService : IRoleFrontPageFormService
{
    private readonly IRoleFrontPageFormRepository _frontPageFormRepository;
    private readonly GeneralSettings _generalSettings;
    private readonly IMemoryCacheService _memoryCacheService;

    public RoleFrontPageFormService(IRoleFrontPageFormRepository frontPageFormRepository,
        IMemoryCacheService memoryCacheService, IOptions<GeneralSettings> generalSettings)
    {
        _frontPageFormRepository = frontPageFormRepository;
        _memoryCacheService = memoryCacheService;
        _generalSettings = generalSettings.Value;
    }

    public RoleFrontPageFromDto ConvertToDto(RoleFrontPageForm roleFrontPageForm)
    {
        var result = roleFrontPageForm.MapTo<RoleFrontPageFromDto>();

        result.FrontPageFormTitle = roleFrontPageForm.FrontPageForm != null
            ? roleFrontPageForm.FrontPageForm.Title
            : "";

        result.FrontPageFormRoute = roleFrontPageForm.FrontPageForm != null
            ? roleFrontPageForm.FrontPageForm.Route
            : "";

        result.RoleName = roleFrontPageForm.ApplicantRole != null
            ? roleFrontPageForm.ApplicantRole.Name
            : "";

        return result;
    }

    public List<RoleFrontPageFromDto> ConvertToDto(List<RoleFrontPageForm> roleFrontPageForm)
    {
        var result = roleFrontPageForm.Select(ConvertToDto).ToList();
        return result;
    }

    public async Task<List<RoleFrontPageForm>> ReadAllFromCache()
    {
        var cacheResult =
            _memoryCacheService.GetObject<List<RoleFrontPageForm>>(_generalSettings.RoleFrontPageFormCacheKey);

        if (cacheResult != null) return cacheResult;
        var roleFrontPageForms = await _frontPageFormRepository.GetAllAsync();
        _memoryCacheService.SetObject(_generalSettings.RoleFrontPageFormCacheKey, roleFrontPageForms,
            _generalSettings.RoleFrontPageFormCacheExpirationMinutes);
        cacheResult = roleFrontPageForms;

        return cacheResult;
    }
}