using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities;
using DakiHunt.DataAccess.Interfaces.Service.Base;

namespace DakiHunt.DataAccess.Interfaces.Service
{
    public interface IHuntService : IServiceBase<Entities.Hunt,IHuntService>
    {
        Task<List<Entities.Hunt>> GetAllWithIds(IEnumerable<long> ids);
    }
}
