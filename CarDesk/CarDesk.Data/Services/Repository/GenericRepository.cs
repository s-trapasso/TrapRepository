using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDesk.Data.Data;
using CarDesk.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDesk.Data.Services.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbContextFactory<CarDeskDbContext> _contextFactory;
       

        public GenericRepository(IDbContextFactory<CarDeskDbContext> contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            
        }
        public async Task AddAsync(T entity)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            await ctx.Set<T>().AddAsync(entity);
            await ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            var entity = await ctx.Set<T>().FindAsync(id);
            if (entity is not null)
            {
                ctx.Remove(entity);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            await using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            return await ctx.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            ctx.Set<T>().Update(entity);
            await ctx.SaveChangesAsync();
        }
    }
}
