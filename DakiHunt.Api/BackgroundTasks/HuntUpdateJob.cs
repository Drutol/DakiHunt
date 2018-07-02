using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DakiHunt.Api.Composition;
using DakiHunt.DataAccess.Entities;
using DakiHunt.DataAccess.Interfaces.Service;
using DakiHunt.Interfaces;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace DakiHunt.Api.BackgroundTasks
{
    public class HuntUpdateJob : IJob
    {
        public const string HuntUpdateJobMapIdsPropertyKey = "HuntIds";

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = ResourceLocator.ObtainLifetimeScope())
            {
                var crawlers = scope.Resolve<IEnumerable<IDomainCrawler>>();
                var searchCrawlers = scope.Resolve<IEnumerable<IDomainSearchCrawler>>();
                var monitor = scope.Resolve<IDomainMonitor>();
                using (var huntService = scope.Resolve<IHuntService>())
                {
                    huntService.ConfigureIncludes().WithChain(query => query.Include(hunt => hunt.HuntedItem.Domain)).Commit();
                    //first we are getting all hunts that are meant to be fired in this interval
                    var hunts = await huntService.GetAllWithIds((long[])context.JobDetail.JobDataMap[HuntUpdateJobMapIdsPropertyKey]);

                    //now we will group them by items they are bound to in case of them repeating
                    foreach (var huntedItems in hunts.GroupBy(hunt => hunt.HuntedItem.Id))
                    {
                        //each domain will be checked in pararell
                        var updateTasks = new List<Task>();
                        foreach (var huntedItemsWithinDomain in huntedItems.GroupBy(hunt => hunt.HuntedItem.Domain,HuntDomain.IdComparer))
                        {
                            foreach (var hunt in huntedItemsWithinDomain)
                            {
                                updateTasks.Add(new Task(async () =>
                                {
                                    using (await monitor.WaitForDomainAccess(huntedItemsWithinDomain.Key))
                                    {
                                        await ProcessItemUpdate(crawlers,searchCrawlers, hunt);
                                    }
                                }));
                           }     
                        }
                        await Task.WhenAll(updateTasks);
                    }
                }                           
            }
        }


        private async Task ProcessItemUpdate(IEnumerable<IDomainCrawler> crawlers,
            IEnumerable<IDomainSearchCrawler> searchCrawlers, Hunt hunt)
        {
            if (hunt.HuntType == Hunt.Type.SingleItem)
            {
                var crawler = crawlers.First(domainCrawler =>
                    domainCrawler.HandledDomain == hunt.HuntedItem.Domain.Uri);

                var currentState = await crawler.ObtainItemState(hunt);
                
                if (currentState != null)
                {
                    var lastState = hunt.HuntedItem.HistoryStates.Last();
                    if (currentState.DiffWithPrevious(lastState))
                    {
                        hunt.HuntedItem.HistoryStates.Add(currentState);                        
                    }
                }
                else //it means it faulted, log was created in crawler
                {
                    
                }
            }
            else
            {

            }
        }
    }
}
