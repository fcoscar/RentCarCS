using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RentCar.Application.Models;
using RentCar.Infraestructure.Models;

namespace RentCat.Auth.Api.Core;

public static class TokenHelper
{
    public static TokenInfo GetToken(UserModel user, string signKey)
    {
        TokenInfo tokenInfo = new TokenInfo();

        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(signKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Mail),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        tokenInfo.FechaExp = (DateTime)tokenDescriptor.Expires;
        tokenInfo.Token = tokenHandler.WriteToken(token);
        
        return tokenInfo;
    }
}