using Microsoft.IdentityModel.Tokens;
using online_selling.Dto;
using online_selling.Interfaces.Tokens;
using online_selling.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace online_selling.Services
{
    public class TokenMaker : ITokenMaker
    {
        public (string, string) MakeToken(UserDto user, SymmetricSecurityKey secretAccessKey, SymmetricSecurityKey secretRefreshKey)
        {
            List<Claim> claims = new List<Claim>();
            if (user.UserType == Enums.UserType.ADMIN) claims.Add(new Claim(ClaimTypes.Role, "admin"));
            if (user.UserType == Enums.UserType.SELLER) claims.Add(new Claim(ClaimTypes.Role, "seller"));
            if (user.UserType == Enums.UserType.BUYER) claims.Add(new Claim(ClaimTypes.Role, "buyer"));

            var signingCredentials = new SigningCredentials(secretAccessKey, SecurityAlgorithms.HmacSha256);
            // Create the access token
            var accessTokenExpiration = DateTime.Now.AddMinutes(20);
            var accessToken = new JwtSecurityToken(
                issuer: "http://localhost:7020",
                claims: claims,
                expires: accessTokenExpiration,
                signingCredentials: signingCredentials
            );
            var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

            // Create the refresh token
            signingCredentials = new SigningCredentials(secretRefreshKey, SecurityAlgorithms.HmacSha256);
            var refreshTokenExpiration = DateTime.Now.AddDays(7);
            var refreshToken = new JwtSecurityToken(
                issuer: "http://localhost:7020",
                claims: claims,
                expires: refreshTokenExpiration,
                signingCredentials: signingCredentials
            );
            var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);
            return (accessTokenString, refreshTokenString);
        }
    }
}
