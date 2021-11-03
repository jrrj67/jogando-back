using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services;

namespace api.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService<LoginResponse, LoginRequest> _loginService;

        public LoginController(ILogger<LoginController> logger, ILoginService<LoginResponse, LoginRequest> loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Authenticate(LoginRequest loginRequest)
        {
            try
            {
                var response = _loginService.Login(loginRequest);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
