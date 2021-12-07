using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Models.Responses;
using JogandoBack.API.Data.Repositories.RefreshTokens;
using JogandoBack.API.Data.Repositories.Users;
using JogandoBack.API.Data.Services.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JogandoBack.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ILoginService<LoginResponse, LoginRequest> _loginService;
        private readonly IDiagnosticContext _diagnosticContext;
        private readonly IUsersRepository _usersRepository;
        private readonly IRefreshTokensRepository _refreshTokenRepository;

        public AuthenticationController(ILogger<AuthenticationController> logger, ILoginService<LoginResponse, LoginRequest> loginService,
            IDiagnosticContext diagnosticContext, IUsersRepository repository, IRefreshTokensRepository refreshTokenRepository)
        {
            _logger = logger;
            _loginService = loginService;
            _diagnosticContext = diagnosticContext;
            _usersRepository = repository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                _logger.LogInformation("Logging user.");

                _diagnosticContext.Set("UserEmail", loginRequest.Email);

                var response = await _loginService.Login(loginRequest);

                _logger.LogInformation("User logged in.");

                return Ok(new Response<LoginResponse>(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return Unauthorized("A problem occured while attempting to login.");
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokensBaseRequest refreshTokenRequest)
        {
            try
            {
                var refreshToken = _refreshTokenRepository.GetByToken(refreshTokenRequest.Token);

                if (refreshToken == null)
                {
                    return NotFound(new Response<string>(null, "Token not found."));
                }

                await _refreshTokenRepository.DeleteAsync(refreshToken.Id);

                var user = _usersRepository.GetById(refreshToken.UserId);

                if (user == null)
                {
                    return NotFound(new Response<string>(null, "User not found."));
                }

                var response = await _loginService.Authenticate(user);

                return Ok(new Response<LoginResponse>(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return Unauthorized("A problem occured while attempting to refresh the token.");
            }
        }

        [HttpDelete("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("id"));

                var token = _refreshTokenRepository.GetByUserId(userId);

                if (token == null)
                {
                    return NotFound(new Response<string>(null, "Token not found."));
                }

                await _refreshTokenRepository.DeleteAsync(token.Id);

                return Ok(new Response<string>(null, "User logged out."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return Unauthorized("A problem occured while attempting to logout.");
            }
        }
    }
}
