using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities;

namespace DakiHunt.Interfaces
{
    public interface IDomainCrawler
    {
        Uri HandledDomain { get; }
        Hunt.Type HandledHunt { get; }

        Task<DakiItemHistoryEvent> ObtainItemState();
    }
}
