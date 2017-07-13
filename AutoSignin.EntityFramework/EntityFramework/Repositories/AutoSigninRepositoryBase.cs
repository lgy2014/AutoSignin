using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace AutoSignin.EntityFramework.Repositories
{
    public abstract class AutoSigninRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<AutoSigninDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected AutoSigninRepositoryBase(IDbContextProvider<AutoSigninDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

    public abstract class AutoSigninRepositoryBase<TEntity> : AutoSigninRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected AutoSigninRepositoryBase(IDbContextProvider<AutoSigninDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
