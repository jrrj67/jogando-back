using AutoMapper;
using JogandoBack.API.Data.Entities;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;

namespace JogandoBack.API.Data.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<UsersRequest, UsersEntity>();

            CreateMap<UsersEntity, UsersResponse>();
        }
    }
}
