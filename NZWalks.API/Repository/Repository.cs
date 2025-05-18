using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repository.IRepository;
using System.Linq;
using System.Linq.Expressions;

namespace NZWalks.API.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly NZWalkDbContext dbContext;
        internal DbSet<T> dbSet;
        public Repository(NZWalkDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
            dbContext.Walks.Include(u => u.Difficulty).Include(u => u.DifficultyId);
            dbContext.Walks.Include(u => u.Region).Include(u => u.RegionId);
        }

        public async Task<T> CreateAsync(T Object)
        {
            await dbSet.AddAsync(Object);
            await dbContext.SaveChangesAsync();
            return Object;
        }

        public async Task<T?> DeleteAsync(Guid id)
        {
            var Object = await dbSet.FindAsync(id);
            if (Object == null)
            {
                return null;
            }
            dbSet.Remove(Object);
            await dbContext.SaveChangesAsync();
            return Object;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query.Where(filter);
            }

            if(!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach(var includeprop in includeProperties
                    .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }
            
            return query.ToList();
        }

        public async Task<T?> GetByIdAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query.Where(filter);
            }
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach(var includeprop in includeProperties
                    .Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                    query =query.Include(includeprop);
                }
            }
            
            return await query.FirstOrDefaultAsync(); // == dbContext.T.Where(u=>u.Id == id).FirstOrDefault();
        }
    }
}
