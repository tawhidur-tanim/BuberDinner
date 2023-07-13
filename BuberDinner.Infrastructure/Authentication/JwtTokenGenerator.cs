using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Common.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator : IJWTTokenGenerator
{

    private readonly IDateTimeProvider _dateTimeprovider;
    private readonly JwtSettings _settings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> options)
    {
        _dateTimeprovider = dateTimeProvider;
        _settings = options.Value;
    }

    public string GenerateJWTToken(Guid userId, string firstName, string lastName)
    {

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key-123456-long-key-many-more")), SecurityAlgorithms.HmacSha256);

        var claims = new[]{

                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
                ,new Claim(JwtRegisteredClaimNames.GivenName, firstName)
                ,new Claim(JwtRegisteredClaimNames.FamilyName, lastName)
                ,new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        var securityToken = new JwtSecurityToken(
            issuer: _settings.Issuer,
            expires: _dateTimeprovider.UtcNow.AddMinutes(_settings.ExpirationInMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );


        return new JwtSecurityTokenHandler().WriteToken(securityToken);

    }
}