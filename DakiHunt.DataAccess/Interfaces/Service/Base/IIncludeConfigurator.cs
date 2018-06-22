namespace DakiHunt.DataAccess.Interfaces.Service.Base
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
