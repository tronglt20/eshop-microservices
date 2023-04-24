using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.DTOs;

namespace Shared.Infrastructure
{
    public class UnitOfWorkBase<T> : IUnitOfWorkBase<T> where T : DbContext
    {
        private readonly UserInfo UserInfo;
        private readonly T Context;
        private readonly IServiceProvider ServiceProvider;

        public UnitOfWorkBase(T context, IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Context = context;

            UserInfo = serviceProvider.GetService<UserInfo>();
        }

        public async Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func)
        {
            if (Context.Database.CurrentTransaction == null)
            {
                var strategy = Context.Database.CreateExecutionStrategy();
                var transResult = await strategy.ExecuteAsync(async () =>
                {
                    using (var trans = await Context.Database.BeginTransactionAsync())
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
            var entries = Context.ChangeTracker.Entries();
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

            int saved = await Context.SaveChangesAsync();
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
            return ServiceProvider.GetService<IBaseRepository<T>>();
        }
        #endregion
    }
}
