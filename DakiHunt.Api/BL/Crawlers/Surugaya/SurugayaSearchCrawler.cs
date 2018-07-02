using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities;
using DakiHunt.Interfaces;

namespace DakiHunt.Api.BL.Crawlers
{
    public class SurugayaSearchCrawler : IDomainSearchCrawler
    {
        public Uri HandledDomain { get; } = new Uri("https://www.suruga-ya.jp");

        public Task<DakiItemSearchHistoryEntry> ObtainItemState()
        {
            throw new NotImplementedException();
        }
    }
}
