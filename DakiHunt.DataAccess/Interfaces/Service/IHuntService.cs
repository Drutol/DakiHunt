using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities;
using DakiHunt.DataAccess.Interfaces.Service.Base;

namespace DakiHunt.DataAccess.Interfaces.Service
{
    public interface IHuntService : IServiceBase<Hunt,IHuntService>
    {
        Task<List<Hunt>> GetAllWithIds(IEnumerable<long> ids);
    }
}
