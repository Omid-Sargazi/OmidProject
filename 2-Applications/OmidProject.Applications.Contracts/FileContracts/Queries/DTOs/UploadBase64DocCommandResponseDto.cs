namespace OmidProject.Applications.Contracts.FileContracts.Queries.DTOs;

public class UploadBase64DocCommandResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
}