using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using online_selling.Dto;
using online_selling.Interfaces.Authentication;
using online_selling.Interfaces.PendingUsers;
using online_selling.Interfaces.Users;
using online_selling.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace online_selling.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IAuthentication _authentication;
        private readonly IPendingUserService _pendingUserService;

        public UserController(IUserService userService, IAuthentication authentication, IPendingUserService pendingUserService)
        {
            _userService = userService;
            _authentication = authentication;
            _pendingUserService = pendingUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterDto registerDto)
        {
            try
            {
                var result = await _userService.Register(registerDto);
                return CreatedAtAction(nameof(RegisterUser),result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred : " + ex.Message);
            }
        }

        [HttpPost("registerGoogle")]
        public async Task<IActionResult> RegisterUserGoogle([FromBody]RegisterGoogleDto registerGoogleDto)
        {
            try
            {
                var token = registerGoogleDto.GoogleToken;
                var validPayload = await GoogleJsonWebSignature.ValidateAsync(token);
                var userEmail = validPayload.Email;
                var userName = validPayload.GivenName;
                var userLastname = validPayload.FamilyName;
                var userPicture = validPayload.Picture;
                RegisterDto registerDto = new RegisterDto();
                registerDto.Email = userEmail;
                registerDto.Name = userName;
                registerDto.Lastname = userLastname;
                registerDto.Picture = userPicture;

                if (_authentication.DoesUserExist(userEmail) == false) {
                    var result = await _userService.RegisterGoogle(userEmail, userName, userLastname, userPicture);
                }
                UserDto user = await _authentication.GetUserByEmail(userEmail);
                string accessToken, refreshToken;
                (accessToken, refreshToken) = _authentication.MakeTokens(user);
                HttpContext.Response.Headers.Add("Auth-Access-Token", accessToken);
                HttpContext.Response.Headers.Add("Auth-Refresh-Token", refreshToken);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListPendingUsers() {
            try
            {
                var result = await _pendingUserService.ListPendingUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("listAll")]
        public async Task<IActionResult> ListAllUsers()
        {
            try
            {
                var result = await _userService.GetAllUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("approve")]
        public async Task<IActionResult> ApproveSellers([FromBody] List<SelectedSellersDto> selectedSellersDtos) {
            try
            {
                var result = await _pendingUserService.ApproveSellers(selectedSellersDtos);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("reject")]
        public async Task<IActionResult> RejectSellers([FromBody] List<SelectedSellersDto> selectedSellersDtos) {
            try
            {
                var result = await _pendingUserService.RejectSellers(selectedSellersDtos);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateProfileDto) {
            try
            {
                var result = await _userService.UpdateProfile(updateProfileDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("status")]
        public async Task<IActionResult> GetSellerStatus([FromBody] EmailDto sellerStatusDto)
        {
            try
            {
                var result = await _userService.GetSellerStatus(sellerStatusDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
