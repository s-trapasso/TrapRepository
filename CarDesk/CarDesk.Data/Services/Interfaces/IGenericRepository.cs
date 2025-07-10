using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarDesk.Data.Services.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null,Expression<Func<T, bool>>? filter = null);
        Task<T?> GetByIdAsync(int id,Func<IQueryable<T>, IQueryable<T>>? include = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
