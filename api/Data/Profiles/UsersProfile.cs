using api.Data.Entities;
using api.Data.Requests;
using api.Data.Responses;
using AutoMapper;

namespace api.Data.Profiles
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
