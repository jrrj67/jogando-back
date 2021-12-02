using AutoMapper;
using JogandoBack.API.Data.Entities;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;

namespace JogandoBack.API.Data.Profiles
{
    public class RefreshTokensProfile : Profile
    {
        public RefreshTokensProfile()
        {
            CreateMap<RefreshTokensRequest, RefreshTokensEntity>();

            CreateMap<RefreshTokensEntity, RefreshTokensResponse>();
        }
    }
}
