using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;
using JogandoBack.API.Data.Services.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace JogandoBack.API.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ILoginService<LoginResponse, LoginRequest> _loginService;
        private readonly IDiagnosticContext _diagnosticContext;

        public AuthenticationController(ILogger<AuthenticationController> logger, ILoginService<LoginResponse, LoginRequest> loginService,
            IDiagnosticContext diagnosticContext)
        {
            _logger = logger;
            _loginService = loginService;
            _diagnosticContext = diagnosticContext;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            try
            {
                _logger.LogInformation("Logging user.");

                _diagnosticContext.Set("UserEmail", loginRequest.Email);

                var response = _loginService.Login(loginRequest);

                _logger.LogInformation("User logged.");

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return Unauthorized(ex.Message);
            }
        }

        //[HttpPost("refresh")]
        //public IActionResult Refresh(RefreshRequest refreshRequest)
        //{

        //}
    }
}
