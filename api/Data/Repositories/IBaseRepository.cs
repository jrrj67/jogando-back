using JogandoBack.API.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JogandoBack.API.Data.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        List<T> GetAll();
        T GetById(int id);
        Task SaveAsync(T item);
        Task UpdateAsync(int id, T item);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }
}