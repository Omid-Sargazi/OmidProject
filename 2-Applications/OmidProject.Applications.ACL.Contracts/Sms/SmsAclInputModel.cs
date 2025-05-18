using static System.Net.Mime.MediaTypeNames;

namespace OmidProject.Applications.ACL.Contracts.Sms;

public class SmsAclInputModel
{
    public string Receiver { get; set; }
    public string Message { get; set; }

    public override string ToString()
    {
        return $"Message: {Message}, Receiver: {Receiver}";
    }
}