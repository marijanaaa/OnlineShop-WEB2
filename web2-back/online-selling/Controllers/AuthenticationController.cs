using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using online_selling.Dto;
using online_selling.Interfaces.Authentication;
using online_selling.Models;
using online_selling.Services;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace online_selling.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        public AuthenticationController(IAuthentication authentication) { 
            _authentication = authentication;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody]LoginDto loginDto) {
            try
            {
                var result = await _authentication.IsAuthenticated(loginDto.Email, loginDto.Password);
                if (result != true) {
                    return NotFound();
                }

                UserDto user = await _authentication.GetUserByEmail(loginDto.Email);
                string accessToken, refreshToken;
                (accessToken, refreshToken) = _authentication.MakeTokens(user);
                HttpContext.Response.Headers.Add("Auth-Access-Token", accessToken);
                HttpContext.Response.Headers.Add("Auth-Refresh-Token", refreshToken);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred : "+ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser(LogoutDto logoutDto)
        {
            try
            {
                var result = await _authentication.Logout(logoutDto.Email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred : " + ex.Message);
            }
        }
    }
}
