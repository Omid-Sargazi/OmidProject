using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;

namespace OmidProject.Infrastructures.Configurations.Authentication;

public class OmidProjectTokenAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
    public const string SchemeName = "OmidProject";
    public const string CustomToken = "OmidProjectToken";

    /// <summary>
    ///     Generates the swagger custom token security scheme object
    /// </summary>
    /// <returns>The swagger custom token security scheme</returns>
    public static OpenApiSecurityScheme GetSwaggerCustomTokenApiSecurityScheme()
    {
        var scheme = new OpenApiSecurityScheme
        {
            Name = CustomToken,
            Type = SecuritySchemeType.ApiKey,
            Scheme = SchemeName,
            In = ParameterLocation.Header,
            Description = "OmidProject Token Authorization Header.\r\n\r\n Example: \"OmidProjectToken : 123456\""
        };
        return scheme;
    }

    /// <summary>
    ///     Generates the swagger security scheme object
    /// </summary>
    /// <returns>The swagger security scheme</returns>
    public static OpenApiSecurityRequirement GetSwaggerCustomTokenSecurityRequirement()
    {
        var req = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = SchemeName
                    }
                },
                new string[] { }
            }
        };
        return req;
    }
}