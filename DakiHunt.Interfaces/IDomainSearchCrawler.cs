using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities;

namespace DakiHunt.Interfaces
{
    public interface IDomainSearchCrawler
    {
        Uri HandledDomain { get; }

        Task<DakiItemSearchHistoryEntry> ObtainItemState();
    }
}
