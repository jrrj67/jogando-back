using AutoMapper;
using JogandoBack.API.Data.Models.Entities;
using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Models.Responses;
using JogandoBack.API.Data.Repositories.Users;
using JogandoBack.API.Data.Services.PasswordHasher;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JogandoBack.API.Data.Services.Users
{
    public class UsersService : IUsersService<UsersResponse, UsersRequest>
    {
        private readonly IUsersRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UsersService(IUsersRepository repository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public List<UsersResponse> GetAll()
        {
            var response = _repository.GetAll();
            return _mapper.Map<List<UsersResponse>>(response);
        }

        public UsersResponse GetById(int id)
        {
            var response = _repository.GetById(id);
            return _mapper.Map<UsersResponse>(response);
        }

        public async Task<UsersResponse> SaveAsync(UsersRequest request)
        {
            var requestModel = _mapper.Map<UsersEntity>(request);
            requestModel.Password = _passwordHasher.HashPassword(requestModel.Password);
            await _repository.SaveAsync(requestModel);
            return _mapper.Map<UsersResponse>(requestModel);
        }

        public async Task<UsersResponse> UpdateAsync(int id, UsersRequest request)
        {
            var requestModel = _mapper.Map<UsersEntity>(request);
            requestModel.Password = _passwordHasher.HashPassword(requestModel.Password);
            await _repository.UpdateAsync(id, requestModel);
            return _mapper.Map<UsersResponse>(requestModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public bool IsUniqueEmail(string email, int userId)
        {
            return _repository.IsUniqueEmail(email, userId);
        }

        public bool Exists(int id)
        {
            return _repository.Exists(id);
        }
    }
}
