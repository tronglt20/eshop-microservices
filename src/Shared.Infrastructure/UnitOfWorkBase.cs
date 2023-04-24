using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.DTOs;

namespace Shared.Infrastructure
{
    public class UnitOfWorkBase<T> : IUnitOfWorkBase<T> where T : DbContext
    {
        private readonly IUserInfo _userInfo;
        private readonly IServiceProvider _serviceProvider;
        private readonly T _context;

        public UnitOfWorkBase(IServiceProvider serviceProvider
            , T context)
        {
            _serviceProvider = serviceProvider;
            _context = context;
            _userInfo = serviceProvider.GetService<IUserInfo>();
        }

        public async Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func)
        {
            if (_context.Database.CurrentTransaction == null)
            {
                var strategy = _context.Database.CreateExecutionStrategy();
                var transResult = await strategy.ExecuteAsync(async () =>
                {
                    using (var trans = await _context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            var result = await func.Invoke();
                            await trans.CommitAsync();
                            return result;
                        }
                        catch (Exception)
                        {
                            await trans.RollbackAsync();
                            throw;
                        }
                    }
                });

                return transResult;
            }
            else
                return await func.Invoke();
        }

        public async Task<int> SaveChangesAsync()
        {
            var entries = _context.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        OnEntryAdded(entry);
                        break;

                    case EntityState.Modified:
                        OnEntryModified(entry);
                        break;
                    case EntityState.Detached:
                    case EntityState.Unchanged:
                    case EntityState.Deleted:
                        break;
                }
            }

            int saved = await _context.SaveChangesAsync();
            return saved;
        }

        private void OnEntryAdded(EntityEntry entry)
        {

        }

        private void OnEntryModified(EntityEntry entry)
        {


        }

        #region Generic Repository
        public virtual IBaseRepository<T> Repository<T>() where T : class
        {
            return _serviceProvider.GetService<IBaseRepository<T>>();
        }
        #endregion
    }
}
