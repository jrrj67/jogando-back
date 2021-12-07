using AutoMapper;
using JogandoBack.API.Data.Models.Constants;
using JogandoBack.API.Data.Models.Entities;
using JogandoBack.API.Data.Models.Filters;
using JogandoBack.API.Data.Models.Filters.Users;
using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Models.Responses;
using JogandoBack.API.Data.Repositories.Users;
using JogandoBack.API.Data.Services.PasswordHasher;
using JogandoBack.API.Data.Services.Uri;
using JogandoBack.API.Data.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JogandoBack.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersRepository _usersRepository;
        private readonly IUriService _uriService;

        public UsersController(ILogger<UsersController> logger, IMapper mapper, IUsersRepository usersRepository, IPasswordHasher passwordHasher,
            IUriService uriService)
        {
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _usersRepository = usersRepository;
            _uriService = uriService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PaginationFilter paginationFilter, [FromQuery] UsersFilter usersFilter)
        {
            try
            {
                var route = Request.Path.Value;

                var filtersList = usersFilter.ToDictionary();

                var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

                var usersEntity = _usersRepository.GetAll(usersFilter, validPaginationFilter);

                var response = _mapper.Map<List<UsersResponse>>(usersEntity);

                var totalRecords = _usersRepository.GetAll(usersFilter).Count;

                var paginatedResponse = PaginationHelper.CreatePagedResponse(response, validPaginationFilter, totalRecords, _uriService, route, filtersList);

                return Ok(paginatedResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(new Response<string>(null, ErrorsConstants.getData));
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var userEntity = _usersRepository.GetById(id);

                var response = _mapper.Map<UsersResponse>(userEntity);

                return Ok(new Response<UsersResponse>(response));
            }
            catch (ArgumentException ex)
            {
                return NotFound(new Response<string>(null, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(new Response<string>(null, ErrorsConstants.getData));
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync(UsersRequest request)
        {
            try
            {
                var userEntity = _mapper.Map<UsersEntity>(request);

                userEntity.Password = _passwordHasher.HashPassword(userEntity.Password);

                await _usersRepository.SaveAsync(userEntity);

                return Created(HttpContext.Request.GetAbsoluteUri() + $"/{userEntity.Id}", new Response<string>(null, MessagesConstants.created));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(new Response<string>(null, ErrorsConstants.saveData));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UsersRequest request)
        {
            try
            {
                var userEntity = _mapper.Map<UsersEntity>(request);

                userEntity.Password = _passwordHasher.HashPassword(userEntity.Password);

                await _usersRepository.UpdateAsync(id, userEntity);

                return Ok(new Response<string>(null, MessagesConstants.updated));
            }
            catch (ArgumentException ex)
            {
                return NotFound(new Response<string>(null, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(new Response<string>(null, ErrorsConstants.updateData));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _usersRepository.DeleteAsync(id);

                return Ok(new Response<string>(null, MessagesConstants.deleted));
            }
            catch (ArgumentException ex)
            {
                return NotFound(new Response<string>(null, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest(new Response<string>(null, ErrorsConstants.deleteData));
            }
        }
    }
}
