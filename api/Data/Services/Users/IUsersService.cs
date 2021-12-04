using JogandoBack.API.Data.Models.Filters;
using JogandoBack.API.Data.Models.Responses;
using System.Collections.Generic;

namespace JogandoBack.API.Data.Services.Users
{
    public interface IUsersService<Response, Request> : IBaseService<Response, Request>
    {
        List<UsersResponse> GetAll(PaginationFilter filter);
        bool IsUniqueEmail(string email, int userId);
    }
}