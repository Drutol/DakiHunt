using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DakiHunt.DataAccess
{
    public delegate IQueryable<TEntity> EntityIncludeDelegate<TEntity>(IQueryable<TEntity> query);
}
