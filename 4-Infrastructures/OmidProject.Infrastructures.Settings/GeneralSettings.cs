namespace OmidProject.Infrastructures.Settings;

public class GeneralSettings
{
    public string DoucmentPath { get; set; }
    public string RoleAccessibleFormCacheKey { get; set; }
    public int RoleAccessibleFormCacheExpirationMinutes { get; set; }
    public string RoleFrontPageFormCacheKey { get; set; }
    public int RoleFrontPageFormCacheExpirationMinutes { get; set; }
}