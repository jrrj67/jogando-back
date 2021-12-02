using AutoMapper;
using JogandoBack.API.Data.Entities;
using JogandoBack.API.Data.Repositories.RefreshTokens;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JogandoBack.API.Data.Services.RefreshTokensEntityService
{
    public class RefreshTokensEntityService : IRefreshTokensEntityService<RefreshTokensResponse, RefreshTokensRequest>
    {
        private readonly IRefreshTokensRepository _repository;
        private readonly IMapper _mapper;

        public RefreshTokensEntityService(IRefreshTokensRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<RefreshTokensResponse> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public RefreshTokensResponse GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public RefreshTokensResponse GetByToken(string token)
        {
            var model = _repository.GetByToken(token);
            return _mapper.Map<RefreshTokensResponse>(model);
        }

        public async Task<RefreshTokensResponse> SaveAsync(RefreshTokensRequest request)
        {
            var requestModel = _mapper.Map<RefreshTokensEntity>(request);
            await _repository.SaveAsync(requestModel);
            return _mapper.Map<RefreshTokensResponse>(requestModel);
        }

        public Task<RefreshTokensResponse> UpdateAsync(int id, RefreshTokensRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
