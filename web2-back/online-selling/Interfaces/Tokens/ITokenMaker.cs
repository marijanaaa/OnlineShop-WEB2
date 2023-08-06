using Microsoft.IdentityModel.Tokens;
using online_selling.Dto;
using online_selling.Models;

namespace online_selling.Interfaces.Tokens
{
    public interface ITokenMaker
    {
        (string, string) MakeToken(UserDto user, SymmetricSecurityKey secretAccessKey, SymmetricSecurityKey secretRefreshKey);
    }
}
