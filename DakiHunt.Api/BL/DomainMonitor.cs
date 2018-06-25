using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities;
using DakiHunt.Interfaces;

namespace DakiHunt.Api.BL
{
    public class DomainMonitor : IDomainMonitor
    {
        private readonly ConcurrentDictionary<Uri,SemaphoreSlim> _domainSemaphoreDictionary = 
            new ConcurrentDictionary<Uri, SemaphoreSlim>();

        public async Task<IDisposable> WaitForDomainAccess(HuntDomain domain)
        {
            if (!_domainSemaphoreDictionary.ContainsKey(domain.Uri))
            {
                _domainSemaphoreDictionary.TryAdd(domain.Uri, new SemaphoreSlim(1));
            }

            var semaphore = _domainSemaphoreDictionary[domain.Uri];
            await semaphore.WaitAsync();
            return new DomainScopeLifetime(semaphore);
        }


        private class DomainScopeLifetime : IDisposable
        {
            private readonly SemaphoreSlim _semaphore;

            public DomainScopeLifetime(SemaphoreSlim semaphore)
            {
                _semaphore = semaphore;
            }

            public void Dispose()
            {
                _semaphore.Release();
            }
        }
    }
}
