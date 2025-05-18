namespace OmidProject.Applications.Contracts.FrontPageFormContracts.Queries.DTOs;

public class RoleFrontPageFromDto
{
    public int Id { get; set; }
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public int FrontPageFormId { get; set; }
    public string FrontPageFormTitle { get; set; }
    public string FrontPageFormRoute { get; set; }
}