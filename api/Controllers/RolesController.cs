using api.Data.Constants;
using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services;
using api.Data.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace api.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IBaseService<RolesResponse, RolesRequest> _rolesService;

        public RolesController(ILogger<RolesController> logger, IBaseService<RolesResponse, RolesRequest> rolesService)
        {
            _logger = logger;
            _rolesService = rolesService;
        }

        [HttpGet]
        [Authorize(Roles = RolesConstants.Admin)]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_rolesService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.Admin)]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_rolesService.GetById(id));
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
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<IActionResult> SaveAsync(RolesRequest request)
        {
            try
            {
                var response = await _rolesService.SaveAsync(request);
                return Created(HttpContext.Request.GetAbsoluteUri() + $"/{response.Id}", "Created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<IActionResult> UpdateAsync(int id, RolesRequest request)
        {
            try
            {
                await _rolesService.UpdateAsync(id, request);
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
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _rolesService.DeleteAsync(id);
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
