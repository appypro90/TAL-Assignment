using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<T> Repository<T>() where T : BaseModel;
        public Task<bool> CompleteAsync();
    }
}
