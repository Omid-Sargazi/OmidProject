namespace OmidProject.Applications.ACL.Contracts.Sms;

public class SendResponseModel
{
    public int status { get; set; }
    public List<object> messages { get; set; }
}