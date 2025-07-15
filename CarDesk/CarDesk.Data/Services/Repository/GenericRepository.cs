using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CarDesk.Data.Data;
using CarDesk.Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CarDesk.Data.Services.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbContextFactory<CarDeskDbContext> _contextFactory;
        

        public GenericRepository(IDbContextFactory<CarDeskDbContext> contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            
        }
        /* ---------- CREATE ---------- */
        public async Task AddAsync(T entity)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            await ctx.Set<T>().AddAsync(entity);
            await ctx.SaveChangesAsync();
        }

        /* ---------- DELETE ---------- */
        public async Task DeleteAsync(int id)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            var entity = await ctx.Set<T>().FindAsync(id);
            if (entity is null) return;

            ctx.Remove(entity);
            await ctx.SaveChangesAsync();
        }

        /* ---------- READ (LISTA) ---------- */
        public async Task<List<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            Expression<Func<T, bool>>? filter = null)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            IQueryable<T> query = ctx.Set<T>().AsNoTracking();

            if (include is not null) query = include(query);
            if (filter is not null) query = query.Where(filter);

            return await query.ToListAsync();
        }

        /* ---------- READ (BY ID) ---------- */
        public async Task<T?> GetByIdAsync(int id,Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            await using var ctx = _contextFactory.CreateDbContext();

            IQueryable<T> query = ctx.Set<T>().AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        /* ---------- UPDATE ---------- */
        public async Task UpdateAsync(T entity)
        {
            await using var ctx = _contextFactory.CreateDbContext();
            ctx.Set<T>().Update(entity);
            await ctx.SaveChangesAsync();
        }
    }
}
