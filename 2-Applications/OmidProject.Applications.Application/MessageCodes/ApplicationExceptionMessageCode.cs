using OmidProject.Frameworks.Contracts.Abstracts.MessageAbstract;

namespace OmidProject.Applications.Application.MessageCodes;

public sealed class ApplicationExceptionMessageCode : ExceptionMessageCode
{
    public ApplicationExceptionMessageCode(int code) : base("ApplicationException", code)
    {
    }

    public static implicit operator ApplicationExceptionMessageCode(int first)
    {
        return new ApplicationExceptionMessageCode(first);
    }
}