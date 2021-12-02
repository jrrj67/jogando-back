using AutoMapper;
using JogandoBack.API.Data.Entities;
using JogandoBack.API.Data.Repositories.RefreshTokens;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JogandoBack.API.Data.Services.RefreshTokensEntityService
{
    public class RefreshTokensEntityService : IRefreshTokensEntityService<RefreshTokenResponse, RefreshTokenRequest>
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

        public List<RefreshTokenResponse> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public RefreshTokenResponse GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public RefreshTokenResponse GetByToken(string token)
        {
            var model = _repository.GetByToken(token);
            return _mapper.Map<RefreshTokenResponse>(model);
        }

        public async Task<RefreshTokenResponse> SaveAsync(RefreshTokenRequest request)
        {
            var requestModel = _mapper.Map<RefreshTokenEntity>(request);
            await _repository.SaveAsync(requestModel);
            return _mapper.Map<RefreshTokenResponse>(requestModel);
        }

        public Task<RefreshTokenResponse> UpdateAsync(int id, RefreshTokenRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
