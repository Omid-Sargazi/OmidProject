using OmidProject.Applications.Contracts.SystemMessageContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Contracts.Common.Enums;

namespace OmidProject.Applications.Contracts.SystemMessageContracts.Commands;

public class UpdateSystemMessageCommand : Command
{
    public TypeSystemMessage TypeMessage { get; set; }

    public int Code { get; set; }

    public List<SystemDataMessageDto> List { get; set; }
}