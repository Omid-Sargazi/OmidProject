namespace OmidProject.Infrastructures.Settings;

public class JwtSetting
{
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public string Secret { get; set; }
    public string Time { get; set; }
}