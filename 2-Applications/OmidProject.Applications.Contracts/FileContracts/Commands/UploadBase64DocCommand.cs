using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using System.Xml.Linq;
using OmidProject.Applications.Contracts.FileContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Common.Enums;

namespace OmidProject.Applications.Contracts.FileContracts.Commands;

public class UploadBase64DocCommand : Command
{
    public DocumentType DocumentType { get; set; }
    public List<UploadBase64DocCommandDto> List { get; set; }
}

public class UploadBase64DocCommandResponse : CommandResponse
{
    public List<UploadBase64DocCommandResponseDto> List { get; set; }
}