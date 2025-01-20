using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void AddItem(T item);
        void UpdateItem(T entity);
        void RemoveItem(T entity);
        Task<bool> SaveAllAsync();
        bool ItemExists(int id);
    }
}
