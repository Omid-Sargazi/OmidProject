using OmidProject.Applications.Contracts.CategoryContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.CategoryContracts.Queries;

public class GetAllCategoriesQuery : Query
{
    public int Direction { get; set; }
}

public class GetAllCategoriesQueryResponse : QueryResponse
{
    public List<CategoryDto> Items { get; set; }
}