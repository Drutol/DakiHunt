using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Database;
using DakiHunt.DataAccess.Interfaces.Service.Base;
using Microsoft.EntityFrameworkCore;

namespace DakiHunt.DataAccess.Services.Base
{
    public abstract partial class ServiceBase<TEntity, TService> : IServiceBase<TEntity, TService>
        where TService : class, IServiceBase<TEntity, TService>
        where TEntity : class
    {
        private readonly bool _saveOnDispose;

        protected DakiDbContext Context { get; private set; }
       
        private EntityIncludeDelegate<TEntity> _includeOverride;

        public ServiceBase(DakiDbContext dbContext, bool saveOnDispose) : base()
        {
            Context = dbContext;
            _saveOnDispose = saveOnDispose;
        }

        private IQueryable<TEntity> InternalInclude(IQueryable<TEntity> query)
        {
            var q = _includeOverride == null ? Include(query) : _includeOverride(query);
            _includeOverride = null;
            return q;
        }

        protected virtual IQueryable<TEntity> Include(IQueryable<TEntity> query)
        {
            return query;
        }



        public List<TEntity> GetAll()
        {
            return InternalInclude(Context.Set<TEntity>()).ToList();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return InternalInclude(Context.Set<TEntity>()).ToListAsync();
        }

        public Task<List<TEntity>> GetAllWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return InternalInclude(Context.Set<TEntity>()).Where(predicate).ToListAsync();
        }

        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return InternalInclude(Context.Set<TEntity>()).First(predicate);
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await InternalInclude(Context.Set<TEntity>()).FirstAsync(predicate);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public TEntity Add(TEntity entity)
        {
            return Context.Add(entity).Entity;
        }

        public void AddRange(IEnumerable<TEntity> items)
        {
            Context.AddRange(items);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }

        public int Count()
        {
            return Context.Set<TEntity>().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Count(predicate);
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            try
            {
                if (Context == null)
                    return;
                if (_saveOnDispose)
                    Context.SaveChanges();
                Context.Dispose();
            }
            catch (ObjectDisposedException)
            {

            }
            finally
            {
                Context = null;
            }
        }

        public IIncludeConfigurator<TEntity, TService> ConfigureIncludes()
        {
            return new IncludeConfigurator<TEntity, TService>(this);
        }
    }
}