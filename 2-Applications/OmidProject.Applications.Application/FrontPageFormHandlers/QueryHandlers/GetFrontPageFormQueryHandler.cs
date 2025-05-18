using OmidProject.Applications.Contracts.FrontPageFormContracts.Queries;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Application.FrontPageFormHandlers.QueryHandlers;

public class GetFrontPageFormQueryHandler : IQueryHandler<GetFrontPageFormQuery, GetFrontPageFormQueryResponse>
{
    private readonly IFrontPageFormRepository _frontPageFormRepository;
    private readonly IFrontPageFormService _frontPageFormService;

    public GetFrontPageFormQueryHandler(IFrontPageFormRepository frontPageFormRepository,
        IFrontPageFormService frontPageFormService)
    {
        _frontPageFormRepository = frontPageFormRepository;
        _frontPageFormService = frontPageFormService;
    }

    public async Task<GetFrontPageFormQueryResponse> Execute(GetFrontPageFormQuery query,
        CancellationToken cancellationToken)
    {
        Guard(query);

        var accessibleForms = await _frontPageFormRepository.GetByPaginationAsync(query.Skip, query.Take);

        var result = new GetFrontPageFormQueryResponse();

        result.Items = _frontPageFormService.ConvertToDto(accessibleForms);

        return result;
    }

    private void Guard(GetFrontPageFormQuery query)
    {
    }
}