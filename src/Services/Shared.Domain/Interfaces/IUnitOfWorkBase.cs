using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Interfaces
{
    public interface IUnitOfWorkBase
    {
        Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func);
        IBaseRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
    }


    public interface IUnitOfWorkBase<T> : IUnitOfWorkBase where T : DbContext
    {
    }
}
