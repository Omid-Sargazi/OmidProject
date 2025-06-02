using OmidProject.Applications.Contracts.CategoryContracts.Queries.DTOs;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface ICategoryService : IService
{
    CategoryDto ConvertTo(Category category);
    List<CategoryDto> ConvertTo(List<Category> categories);

    List<CategoryDto> GenerateTree(List<Category> categories);
    List<CategoryDto> GenerateTreeUpToDown(List<Category> categories);
    List<CategoryDto> GenerateTreeDownToUp(List<Category> categories);
}