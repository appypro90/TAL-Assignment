using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAllAsync();
        Task Upsert(T entity);
    }
}
