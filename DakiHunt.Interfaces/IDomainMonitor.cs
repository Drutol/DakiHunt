using System;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities;

namespace DakiHunt.Interfaces
{
    public interface IDomainMonitor
    {
        /// <summary>
        /// Waits for domain access. Returned object should be disposed upon finishing domain interaction.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        Task<IDisposable> WaitForDomainAccess(HuntDomain domain);
    }
}
