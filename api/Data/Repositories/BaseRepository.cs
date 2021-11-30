using JogandoBack.API.Data.Contexts;
using JogandoBack.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JogandoBack.API.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            var item = _context.Set<T>().FirstOrDefault(t => t.Id == id);

            if (item == null)
            {
                throw new ArgumentException("Not found.");
            }

            return item;
        }

        public async Task SaveAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, T item)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var itemModel = _context.Set<T>().FirstOrDefault(i => i.Id == id);

            if (itemModel == null)
            {
                throw new ArgumentException("Not found.");
            }

            item.Id = itemModel.Id;
            item.CreatedAt = itemModel.CreatedAt;

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = _context.Set<T>().FirstOrDefault(t => t.Id == id);

            if (item == null)
            {
                throw new ArgumentException("Not found.");
            }

            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.Set<T>().Where(t => t.Id == id).Any();
        }
    }
}
