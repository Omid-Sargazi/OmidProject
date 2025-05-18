namespace OmidProject.Infrastructures.Settings;

public class SmsSetting
{
    public string BaseUrl { get; set; }
    public string ResourceUrl { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Domain { get; set; }
    public string Sender { get; set; }
    public string ActivatingRegistrationMessage { get; set; }
    public int SuccessCode { get; set; }
}