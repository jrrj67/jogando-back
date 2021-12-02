using AutoMapper;
using JogandoBack.API.Data.Models.Entities;
using JogandoBack.API.Data.Models.Requests;
using JogandoBack.API.Data.Models.Responses;

namespace JogandoBack.API.Data.Config.Profiles
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<RolesRequest, RolesEntity>();

            CreateMap<RolesEntity, RolesResponse>();
        }
    }
}
