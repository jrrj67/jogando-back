using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Entities;
using api.Data.Repositories;
using api.Data.Requests;
using api.Data.Responses;

namespace api.Data.Services
{
    public class UsersService : IUsersService<UsersResponse, UsersRequest>
    {
        private readonly IUsersRepository _repository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            requestModel.Password = BCrypt.Net.BCrypt.HashPassword(requestModel.Password);
            await _repository.SaveAsync(requestModel);
            return _mapper.Map<UsersResponse>(requestModel);
        }

        public async Task<UsersResponse> UpdateAsync(int id, UsersRequest request)
        {
            var requestModel = _mapper.Map<UsersEntity>(request);
            requestModel.Password = BCrypt.Net.BCrypt.HashPassword(requestModel.Password);
            await _repository.UpdateAsync(id, requestModel);
            return _mapper.Map<UsersResponse>(requestModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public bool IsUniqueEmail(string email)
        {
            return _repository.IsUniqueEmail(email);
        }
    }
}
