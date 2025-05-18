using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.AccessibleFormContracts.Queries;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.QueryHandlers;

public class
    GetRoleAccessibleFormQueryHandler : IQueryHandler<GetRoleAccessibleFormQuery, GetRoleAccessibleFormQueryResponse>
{
    private readonly IRoleAccessibleFormRepository _roleAccessibleFormRepository;
    private readonly IRoleAccessibleFormService _roleAccessibleFormService;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public GetRoleAccessibleFormQueryHandler(IRoleAccessibleFormRepository roleAccessibleFormRepository,
        IRoleAccessibleFormService roleAccessibleFormService, RoleManager<ApplicationRole> roleManager)
    {
        _roleAccessibleFormRepository = roleAccessibleFormRepository;
        _roleAccessibleFormService = roleAccessibleFormService;
        _roleManager = roleManager;
    }

    public async Task<GetRoleAccessibleFormQueryResponse> Execute(GetRoleAccessibleFormQuery query,
        CancellationToken cancellationToken)
    {
        Guard(query);

        if (_roleManager.FindByIdAsync(query.RoleId.ToString()) == null) throw new RoleNotFoundException();

        var roleAccessibleForms = await _roleAccessibleFormRepository.GetWithRoleId(query.RoleId);
        if (roleAccessibleForms == null) throw new RoleAccessibleFormNotFoundException();

        var result = new GetRoleAccessibleFormQueryResponse
        {
            Items = _roleAccessibleFormService.ConvertToDto(roleAccessibleForms)
        };

        return result;
    }

    private void Guard(GetRoleAccessibleFormQuery query)
    {
        if (query.RoleId.IsEmpty()) throw new IdIsInvalidIdException();
    }
}