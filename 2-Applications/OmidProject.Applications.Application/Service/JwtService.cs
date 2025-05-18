using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Infrastructures.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace OmidProject.Applications.Application.Service;

public class JwtService : IJwtService
{
    private readonly JwtSetting _jwtSetting;

    public JwtService(IOptions<JwtSetting> jwtSetting)
    {
        _jwtSetting = jwtSetting.Value;
    }

    public string GenerateJwtToken(ApplicationUser user, IList<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSetting.Secret);
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Actor, user.IsSuperAdmin.ToString()));

        if (roles != null && roles.Any()) claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenDescriptor = new SecurityTokenDescriptor();
        tokenDescriptor.Subject = new ClaimsIdentity(claims);
        tokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtSetting.Time));
        tokenDescriptor.SigningCredentials =
            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}