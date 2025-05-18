using OmidProject.Applications.Contracts.AccessibleFormContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Utilities.Extensions;
using OmidProject.Infrastructures.Settings;
using Microsoft.Extensions.Options;

namespace OmidProject.Applications.Application.Service;

public class RoleAccessibleFormService : IRoleAccessibleFormService
{
    private readonly IRoleAccessibleFormRepository _roleAccessibleFormRepository;
    private readonly IMemoryCacheService _memoryCacheService;
    private readonly GeneralSettings _generalSettings;

    public RoleAccessibleFormService(IRoleAccessibleFormRepository roleAccessibleFormRepository, IMemoryCacheService memoryCacheService, IOptions<GeneralSettings> generalSettings)
    {
        _roleAccessibleFormRepository = roleAccessibleFormRepository;
        _memoryCacheService = memoryCacheService;
        _generalSettings = generalSettings.Value;
    }

    public RoleAccessibleFormDto ConvertToDto(RoleAccessibleForm roleAccessibleForm)
    {
        var result = roleAccessibleForm.MapTo<RoleAccessibleFormDto>();

        result.AccessibleFormTitle = roleAccessibleForm.AccessibleForm != null
            ? roleAccessibleForm.AccessibleForm.Title
            : "";

        result.AccessibleFormRoute = roleAccessibleForm.AccessibleForm != null
            ? roleAccessibleForm.AccessibleForm.Route
            : "";

        result.RoleName = roleAccessibleForm.ApplicantRole != null
            ? roleAccessibleForm.ApplicantRole.Name
            : "";

        return result;
    }

    public List<RoleAccessibleFormDto> ConvertToDto(List<RoleAccessibleForm> roleAccessibleForm)
    {
        var result = roleAccessibleForm.Select(ConvertToDto).ToList();
        return result;
    }

    public async Task<List<RoleAccessibleForm>> ReadAllFromCache()
    {
        var cacheResult =
            _memoryCacheService.GetObject<List<RoleAccessibleForm>>(_generalSettings.RoleAccessibleFormCacheKey);

        if (cacheResult != null) return cacheResult;

        var roleAccessibleForms = await _roleAccessibleFormRepository.GetAllAsync();
        _memoryCacheService.SetObject(_generalSettings.RoleAccessibleFormCacheKey, roleAccessibleForms);
        cacheResult = roleAccessibleForms;

        return cacheResult;
    }

    public async Task ResetCache()
    {
        _memoryCacheService.Delete(_generalSettings.RoleAccessibleFormCacheKey);

        _memoryCacheService.SetObject(
            _generalSettings.RoleAccessibleFormCacheKey,
            await _roleAccessibleFormRepository.GetAllAsync()
        );
    }
}