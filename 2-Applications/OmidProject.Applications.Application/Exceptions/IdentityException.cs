using OmidProject.Applications.Application.MessageCodes;
using OmidProject.Frameworks.Contracts.Abstracts.ExceptionStructureMessages;

namespace OmidProject.Applications.Application.Exceptions;

public sealed class IdentityException : BusinessException
{
    public IdentityException(string? persianMessage, string englishMessage)
        : base(ExceptionCodes.Identity.IdentityError)
    {
        if (persianMessage is null)
        {
            PersianMessage = englishMessage.Split("@")[0];
            EnglishMessage = englishMessage.Split("@")[1];
        }
        else
        {
            PersianMessage = persianMessage;
            EnglishMessage = englishMessage;
        }


        Message = PersianMessage;
    }

    public string? PersianMessage { get; }
    public string EnglishMessage { get; }
    public string Message { get; }
}