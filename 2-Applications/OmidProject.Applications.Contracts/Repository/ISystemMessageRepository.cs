using OmidProject.Domains.Domain.SystemMessages;
using OmidProject.Frameworks.Contracts.Common.Enums;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Repository;

public interface ISystemMessageRepository : IRepository
{
    Task<SystemMessage> GetMessageByCodeAndType(int code, TypeSystemMessage type);
    Task<SystemDataMessage> GetDataMessageByCodeAndType(int code, TypeSystemMessage type, ContentLanguage language);
    Task<SystemMessage> GetMessageByCode(int code);
    Task<bool> Create(SystemMessage systemMessage);
    Task<bool> Update(SystemMessage systemMessage);
    Task<bool> Delete(int code);
    Task<List<SystemMessage>> GetAll();
}