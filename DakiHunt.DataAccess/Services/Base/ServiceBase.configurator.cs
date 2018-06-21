using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DakiHunt.DataAccess.Interfaces;

namespace DakiHunt.DataAccess.Services
{
    public abstract partial class ServiceBase<TEntity, TService> : IServiceBase<TEntity, TService>
        where TService : class, IServiceBase<TEntity, TService>
        where TEntity : class
    {
        public class IncludeConfigurator<TEntity, TService> : IIncludeConfigurator<TEntity, TService>
            where TService : class, IServiceBase<TEntity, TService>
            where TEntity : class
        {
            private readonly ServiceBase<TEntity, TService> _parent;
            private EntityIncludeDelegate<TEntity> _includeChain;

            internal IncludeConfigurator(ServiceBase<TEntity, TService> parent)
            {
                _parent = parent;
            }

            public IIncludeConfigurator<TEntity, TService> WithChain(EntityIncludeDelegate<TEntity> chain)
            {
                _includeChain = chain;
                return this;
            }

            public IIncludeConfigurator<TEntity, TService> ExtendChain(EntityIncludeDelegate<TEntity> chain)
            {
                _includeChain = query => chain(_parent.Include(query));
                return this;
            }

            public IIncludeConfigurator<TEntity, TService> IgnoreDefaultServiceIncludes()
            {
                _includeChain = IncludeOverride;
                return this;
            }

            public void Commit()
            {
                _parent._includeOverride = _includeChain;
            }

            private IQueryable<TEntity> IncludeOverride(IQueryable<TEntity> query)
            {
                return query;
            }
        }
    }
}
