using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace api.Controllers
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
                var response = _loginService.Login(loginRequest);
                _diagnosticContext.Set("UserEmail", loginRequest.Email);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
