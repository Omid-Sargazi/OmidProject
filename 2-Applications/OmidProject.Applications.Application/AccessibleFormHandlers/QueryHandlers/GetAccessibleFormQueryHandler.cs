using OmidProject.Applications.Contracts.AccessibleFormContracts.Queries;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Application.AccessibleFormHandlers.QueryHandlers;

public class GetAccessibleFormQueryHandler : IQueryHandler<GetAccessibleFormQuery, GetAccessibleFormQueryResponse>
{
    private readonly IAccessibleFormRepository _accessibleFormRepository;
    private readonly IAccessibleFormService _accessibleFormService;

    public GetAccessibleFormQueryHandler(IAccessibleFormRepository accessibleFormRepository,
        IAccessibleFormService accessibleFormService)
    {
        _accessibleFormRepository = accessibleFormRepository;
        _accessibleFormService = accessibleFormService;
    }

    public async Task<GetAccessibleFormQueryResponse> Execute(GetAccessibleFormQuery query,
        CancellationToken cancellationToken)
    {
        Guard(query);

        var accessibleForms = await _accessibleFormRepository.GetByPaginationAsync(query.Skip, query.Take);

        var result = new GetAccessibleFormQueryResponse();

        result.Items = _accessibleFormService.ConvertToDto(accessibleForms);

        return result;
    }

    private void Guard(GetAccessibleFormQuery query)
    {
    }
}