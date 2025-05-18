using OmidProject.Frameworks.Contracts.Abstracts.MessageAbstract;

namespace OmidProject.Frameworks.Contracts.MessageCodeAbstract;

public static class ResponseCodes
{
    public static FrameworkResponseMessageCode OperationSuccessful = new CodeAndMessage
        {Code = 1, Message = "عملیات با موفقیت انجام شد"};

    public static FrameworkResponseMessageCode OperationFailed = new CodeAndMessage
        {Code = 2, Message = "عملیات ناموفق بود"};

    public static FrameworkResponseMessageCode OperationCanceled = new CodeAndMessage
        {Code = 3, Message = "عملیات لغو شد"};
}