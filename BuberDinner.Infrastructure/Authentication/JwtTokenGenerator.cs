using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator : IJWTTokenGenerator
{
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
            issuer: "BuberDinner",
            expires: DateTime.Now.AddDays(1),
            claims: claims,
            signingCredentials: signingCredentials
        );


        return new JwtSecurityTokenHandler().WriteToken(securityToken);

    }
}