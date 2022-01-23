using Dal;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SlotsDbContext _context;
        public Dictionary<Type, object> repositories = new();

        public UnitOfWork(SlotsDbContext bookStoreDbContext)
        {
            _context = bookStoreDbContext;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseModel
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IGenericRepository<T>;
            }

            IGenericRepository<T> repo = new GenericRepository<T>(_context);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public async Task<bool> CompleteAsync() => (await _context.SaveChangesAsync()) > 0;

        public void Dispose() => _context.Dispose();
    }
}
