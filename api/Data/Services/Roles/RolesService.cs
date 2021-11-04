using api.Data.Entities;
using api.Data.Repositories;
using api.Data.Requests;
using api.Data.Responses;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Data.Services.Roles
{
    public class RolesService : IBaseService<RolesResponse, RolesRequest>
    {
        private readonly IBaseRepository<RolesEntity> _repository;
        private readonly IMapper _mapper;

        public RolesService(IBaseRepository<RolesEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<RolesResponse> GetAll()
        {
            var response = _repository.GetAll();
            return _mapper.Map<List<RolesResponse>>(response);
        }

        public RolesResponse GetById(int id)
        {
            var response = _repository.GetById(id);
            return _mapper.Map<RolesResponse>(response);
        }

        public async Task<RolesResponse> SaveAsync(RolesRequest request)
        {
            var requestModel = _mapper.Map<RolesEntity>(request);
            await _repository.SaveAsync(requestModel);
            return _mapper.Map<RolesResponse>(requestModel);
        }

        public async Task<RolesResponse> UpdateAsync(int id, RolesRequest request)
        {
            var requestModel = _mapper.Map<RolesEntity>(request);
            await _repository.UpdateAsync(id, requestModel);
            return _mapper.Map<RolesResponse>(requestModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
