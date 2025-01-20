using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;
        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public void AddItem(T item)
        {
            _storeContext.Set<T>().AddAsync(item);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _storeContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _storeContext.Set<T>().FindAsync(id);
        }

        public bool ItemExists(int id)
        {
            return _storeContext.Set<T>().Any(e => e.Id == id);
        }

        public void RemoveItem(T entity)
        {
            _storeContext.Set<T>().Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _storeContext.SaveChangesAsync() > 0;
        }

        public void UpdateItem(T item)
        {
            _storeContext.Set<T>().Attach(item);
            _storeContext.Entry(item).State = EntityState.Modified;
        }
    }
}
