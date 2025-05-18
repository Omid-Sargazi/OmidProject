using OmidProject.Applications.Contracts.FileContracts.Queries;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Application.FileHandlers.QueryHandlers;

public class GetBase64DocQueryHandler : IQueryHandler<GetBase64DocQuery, GetBase64DocQueryResponse>
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IDocumentService _documentService;

    public GetBase64DocQueryHandler(IDocumentRepository documentRepository, IDocumentService documentService)
    {
        _documentRepository = documentRepository;
        _documentService = documentService;
    }

    public async Task<GetBase64DocQueryResponse> Execute(GetBase64DocQuery query, CancellationToken cancellationToken)
    {
        var dto = _documentService.GenerateFileDto(query.DocumentId);

        var result = new GetBase64DocQueryResponse();
        result.Item = dto;

        return result;
    }
}