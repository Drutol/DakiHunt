using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities;
using DakiHunt.Interfaces;

namespace DakiHunt.Api.BL.Crawlers
{
    public class SurugayaCrawler : IDomainCrawler
    {
        public Uri HandledDomain { get; }

        public Hunt.Type HandledHunt { get; } = Hunt.Type.SingleItem;

        public Task<DakiItemHistoryEvent> ObtainItemState()
        {
            throw new NotImplementedException();
        }
    }
}
