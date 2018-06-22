using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Database;
using DakiHunt.DataAccess.Entities;
using DakiHunt.DataAccess.Interfaces.Service;
using DakiHunt.DataAccess.Services.Base;

namespace DakiHunt.DataAccess.Services
{
    public class HuntService : ServiceBase<Hunt,IHuntService> , IHuntService
    {
        public HuntService(DakiDbContext dbContext, bool saveOnDispose) : base(dbContext, saveOnDispose)
        {
        }

        public async Task<List<Hunt>> GetAllWithIds(IEnumerable<long> ids)
        {
            return await GetAllWhereAsync(hunt => ids.Any(l => l == hunt.Id));
        }
    }
}
