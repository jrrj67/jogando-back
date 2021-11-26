using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services.Users;
using api.Data.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService<UsersResponse, UsersRequest> _usersService;

        public UsersController(ILogger<UsersController> logger, IUsersService<UsersResponse, UsersRequest> usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_usersService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_usersService.GetById(id));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync(UsersRequest request)
        {
            try
            {
                var response = await _usersService.SaveAsync(request);
                return Created(HttpContext.Request.GetAbsoluteUri() + $"/{response.Id}", "Created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UsersRequest request)
        {
            try
            {
                await _usersService.UpdateAsync(id, request);
                return Ok("Updated.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _usersService.DeleteAsync(id);
                return Ok("Deleted.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
