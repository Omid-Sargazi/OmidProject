namespace OmidProject.Applications.Contracts.FileContracts.Queries.DTOs;

public class GetFileDto
{
    public string Content { get; set; }
    public string FileName { get; set; }
    public Guid Id { get; set; }
}