using AutoMapper;
using api.Data.Entities;
using api.Data.Requests;
using api.Data.Responses;

namespace api.Data.Profiles
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
