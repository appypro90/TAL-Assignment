using Dal;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        protected SlotsDbContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(
            SlotsDbContext slotContext)
        {
            context = slotContext;
            dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task Upsert(T entity)
        {
            var existingEntity = (await FindAsync(x => x.Id == entity.Id)).FirstOrDefault();
            if (existingEntity != null)
            {
                existingEntity = entity;
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                dbSet.Add(entity);
            }
        }
    }
}
