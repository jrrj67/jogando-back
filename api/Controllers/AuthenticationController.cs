using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Models.Responses;
using JogandoBack.API.Data.Repositories.Users;
using JogandoBack.API.Data.Services.Login;
using JogandoBack.API.Data.Services.RefreshTokensEntityService;
using JogandoBack.API.Data.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace JogandoBack.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ILoginService<LoginResponse, LoginRequest> _loginService;
        private readonly IRefreshTokensEntityService<RefreshTokensResponse, RefreshTokensRequest> _refreshTokensEntityService;
        private readonly IDiagnosticContext _diagnosticContext;
        private readonly IUsersRepository _repository;

        public AuthenticationController(ILogger<AuthenticationController> logger, ILoginService<LoginResponse, LoginRequest> loginService,
            IDiagnosticContext diagnosticContext, IRefreshTokensEntityService<RefreshTokensResponse, RefreshTokensRequest> refreshTokensEntityService, 
            IUsersRepository repository)
        {
            _logger = logger;
            _loginService = loginService;
            _diagnosticContext = diagnosticContext;
            _refreshTokensEntityService = refreshTokensEntityService;
            _repository = repository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                _logger.LogInformation("Logging user.");

                _diagnosticContext.Set("UserEmail", loginRequest.Email);

                var response = await _loginService.Login(loginRequest);

                _logger.LogInformation("User logged.");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokensBaseRequest refreshTokenRequest)
        {
            var refreshToken = _refreshTokensEntityService.GetByToken(refreshTokenRequest.Token);

            if (refreshToken == null)
            {
                return NotFound("Token not found");
            }

            await _refreshTokensEntityService.DeleteAsync(refreshToken.Id);

            var user = _repository.GetById(refreshToken.UserId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var response = await _loginService.Authenticate(user);

            return Ok(response);
        }
    }
}
