using AutoMapper;
using JogandoBack.API.Data.Models.Constants;
using JogandoBack.API.Data.Models.Entities;
using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Models.Responses;
using JogandoBack.API.Data.Repositories;
using JogandoBack.API.Data.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JogandoBack.API.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;
        private readonly IBaseRepository<RolesEntity> _rolesRepository;

        public RolesController(ILogger<RolesController> logger, IBaseRepository<RolesEntity> rolesRepository, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _rolesRepository = rolesRepository;
        }

        [HttpGet]
        [Authorize(Roles = RolesConstants.Admin)]
        public IActionResult GetAll()
        {
            try
            {
                var rolesEntity = _rolesRepository.GetAll();

                var response = _mapper.Map<List<RolesResponse>>(rolesEntity);

                return Ok(new Response<List<RolesResponse>>(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(new Response<string>(null, ex.Message));
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RolesConstants.Admin)]
        public IActionResult GetById(int id)
        {
            try
            {
                var rolesEntity = _rolesRepository.GetById(id);

                var response = _mapper.Map<UsersResponse>(rolesEntity);

                return Ok(new Response<UsersResponse>(response));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<IActionResult> SaveAsync(RolesRequest request)
        {
            try
            {
                var roleEntity = _mapper.Map<RolesEntity>(request);

                await _rolesRepository.SaveAsync(roleEntity);

                return Created(HttpContext.Request.GetAbsoluteUri() + $"/{roleEntity.Id}", new Response<string>(null, "Created."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<IActionResult> UpdateAsync(int id, RolesRequest request)
        {
            try
            {
                var roleEntity = _mapper.Map<RolesEntity>(request);

                await _rolesRepository.UpdateAsync(id, roleEntity);

                return Ok(new Response<string>(null, "Updated."));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _rolesRepository.DeleteAsync(id);

                return Ok(new Response<string>(null, "Deleted."));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
