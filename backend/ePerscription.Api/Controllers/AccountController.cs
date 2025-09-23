using Azure.Core;
using ePerscription.Api.Common;
using ePerscription.Api.Configuration;
using ePerscription.Application.DTOs;
using ePerscription.Application.Interfaces;
using ePerscription.Application.Request;
using ePerscription.Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Exim.WebApi.Controllers
{

   
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        // Dependency injection for the account service
        private readonly IAccountService _accountService;
        private IOptions<AppSettings> _appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="accountService">The account service.</param>
        public AccountController(IAccountService accountService, IOptions<AppSettings> appSettings)
        {
            _accountService = accountService;
            _appSettings = appSettings;
        }

        [HttpPost("register")]
        public async Task<ApiResponse<UserDto>> Register(UserDto user, string password)
        {
            var apiResponse = new ApiResponse<UserDto>();

            try
            {
                var createdUser = await _accountService.Register(user, password);
                apiResponse.Status = createdUser == null ? false : true;
                apiResponse.Message = createdUser == null ? "Not a valid credentials" : "Credentials Authenticated";
                apiResponse.Data = createdUser;

            }
            catch (SqlException ex)
            {
                apiResponse.Status = false;
                apiResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.Status = false;
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }
        /// <summary>
        /// Authenticates a user based on the provided login request.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <returns>A success response if the login is successful, otherwise a failure response.</returns>
        [HttpPost("Login")]
        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest request)
        {
            

            var apiResponse = new ApiResponse<LoginResponse>();

            try
            {
                var user = await _accountService.Login(request.Email, request.Password);

                var token = GenerateJwtToken(request.Email);

                apiResponse.Status = user == null ? false : true;
                apiResponse.Message = user == null ? "Not a valid credentials" : "Credentials Authenticated";
                apiResponse.Data = new LoginResponse() { Token=token, UserDetails=user};

            }
            catch (SqlException ex)
            {
                apiResponse.Status = false;
                apiResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                apiResponse.Status = false;
                apiResponse.Message = ex.Message;
            }

            return apiResponse;
        }

        /// <summary>
        /// Generates a JWT token for the specified email.
        /// </summary>
        /// <param name="email">The email for which the token is generated.</param>
        /// <returns>A JWT token as a string.</returns>
        private string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _appSettings.Value.Secret;
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }),
                Expires = DateTime.UtcNow.AddHours(_appSettings.Value.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
