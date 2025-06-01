using OmidProject.Applications.Contracts.CategoryContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.Service;

public class CategoryService : ICategoryService
{
    public CategoryDto ConvertTo(Category category)
    {
        var result = category.MapTo<CategoryDto>();

        result.Parent = category.Parent != null ? ConvertTo(category.Parent) : new CategoryDto();
        result.Child = category.Child != null ? ConvertTo(category.Child) : new List<CategoryDto>();

        return result;
    }

    public List<CategoryDto> ConvertTo(List<Category> categories)
    {
        return categories.Select(ConvertTo).ToList();
    }
}