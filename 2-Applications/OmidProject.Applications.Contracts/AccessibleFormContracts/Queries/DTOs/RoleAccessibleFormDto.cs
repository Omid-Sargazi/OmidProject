namespace OmidProject.Applications.Contracts.AccessibleFormContracts.Queries.DTOs;

public class RoleAccessibleFormDto
{
    public int Id { get; set; }
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public int AccessibleFormId { get; set; }
    public string AccessibleFormTitle { get; set; }
    public string AccessibleFormRoute { get; set; }
}