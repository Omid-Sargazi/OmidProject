namespace OmidProject.Applications.Contracts.UserManagerContracts.Queries.DTOs;

public class ApplicationUserDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string? MobileNumber { get; set; }
}