using System;
using System.Collections.Generic;
using System.Text;

namespace DakiHunt.DataAccess.Interfaces
{
    public interface IIncludeConfigurator<TEntity, TService>
        where TEntity : class
        where TService : class, IServiceBase<TEntity, TService>
    {
        void Commit();

        IIncludeConfigurator<TEntity, TService> ExtendChain(EntityIncludeDelegate<TEntity> chain);
        IIncludeConfigurator<TEntity, TService> WithChain(EntityIncludeDelegate<TEntity> chain);
        IIncludeConfigurator<TEntity, TService> IgnoreDefaultServiceIncludes();
    }
}
