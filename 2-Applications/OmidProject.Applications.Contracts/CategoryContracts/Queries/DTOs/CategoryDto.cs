namespace OmidProject.Applications.Contracts.CategoryContracts.Queries.DTOs;

public class CategoryDto
{
    public CategoryDto()
    {
        Child = new List<CategoryDto>();
    }
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public int Level { get; set; }
    //public CategoryDto Parent { get; set; }
    public List<CategoryDto> Child { get; set; }
}