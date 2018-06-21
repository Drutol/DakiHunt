using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DakiHunt.DataAccess.Interfaces
{
    public interface IServiceBase<TEntity, TService> : IDisposable
        where TEntity : class
        where TService : class, IServiceBase<TEntity, TService>
    {
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllWhereAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity First(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);


        TEntity Add(TEntity client);
        void AddRange(IEnumerable<TEntity> items);
        void Remove(TEntity client);
        void Update(TEntity client);
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);

        Task SaveChangesAsync();

        IIncludeConfigurator<TEntity, TService> ConfigureIncludes();
    }
}