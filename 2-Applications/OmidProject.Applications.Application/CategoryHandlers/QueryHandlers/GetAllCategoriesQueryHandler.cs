using OmidProject.Applications.Contracts.CategoryContracts.Queries;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Application.CategoryHandlers.QueryHandlers;

public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, GetAllCategoriesQueryResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryService _categoryService;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, ICategoryService categoryService)
    {
        _categoryRepository = categoryRepository;
        _categoryService = categoryService;
    }

    public async Task<GetAllCategoriesQueryResponse> Execute(GetAllCategoriesQuery query,
        CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync();
        var result = new GetAllCategoriesQueryResponse();
        result.Items = 
            query.Direction == 1 ? _categoryService.GenerateTreeUpToDown(categories) :
            query.Direction == 2 ? _categoryService.GenerateTreeDownToUp(categories) :
            _categoryService.GenerateTree(categories);
        return result;
    }
}