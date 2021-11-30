using System.Collections.Generic;
using System.Threading.Tasks;

namespace JogandoBack.API.Data.Services
{
    public interface IBaseService<Response, Request>
    {
        List<Response> GetAll();
        Response GetById(int id);
        Task<Response> SaveAsync(Request request);
        Task<Response> UpdateAsync(int id, Request request);
        Task DeleteAsync(int id);
    }
}