using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Application.FrontPageFormHandlers.CommandHandlers.Exception;
using OmidProject.Applications.Contracts.FrontPageFormContracts.Queries;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;

namespace OmidProject.Applications.Application.FrontPageFormHandlers.QueryHandlers;

public class
    GetRoleFrontPageFormQueryHandler : IQueryHandler<GetRoleFrontPageFormQuery, GetRoleFrontPageFormQueryResponse>
{
    private readonly IRoleFrontPageFormRepository _frontPageFormRepository;
    private readonly IRoleFrontPageFormService _frontPageFormService;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public GetRoleFrontPageFormQueryHandler(IRoleFrontPageFormRepository frontPageFormRepository,
        IRoleFrontPageFormService frontPageFormService, RoleManager<ApplicationRole> roleManager)
    {
        _frontPageFormRepository = frontPageFormRepository;
        _frontPageFormService = frontPageFormService;
        _roleManager = roleManager;
    }


    public async Task<GetRoleFrontPageFormQueryResponse> Execute(GetRoleFrontPageFormQuery query,
        CancellationToken cancellationToken)
    {
        Guard(query);

        if (_roleManager.FindByIdAsync(query.RoleId.ToString()) == null) throw new RoleNotFoundException();

        var roleFrontPageForms = await _frontPageFormRepository.GetWithRoleId(query.RoleId);
        if (roleFrontPageForms == null) throw new RoleFrontPageFormNotFoundException();

        var result = new GetRoleFrontPageFormQueryResponse
        {
            Items = _frontPageFormService.ConvertToDto(roleFrontPageForms)
        };

        return result;
    }

    private void Guard(GetRoleFrontPageFormQuery query)
    {
        if (query.RoleId.IsEmpty()) throw new IdIsInvalidIdException();
    }
}