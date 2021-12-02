using AutoMapper;
using JogandoBack.API.Data.Entities;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;

namespace JogandoBack.API.Data.Profiles
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshTokenRequest, RefreshTokenEntity>();

            CreateMap<RefreshTokenEntity, RefreshTokenResponse>();
        }
    }
}
