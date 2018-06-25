using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Database;
using DakiHunt.DataAccess.Entities.Auth;
using Microsoft.AspNetCore.Identity;

namespace DakiHunt.DataAccess.Interfaces
{
    public interface IDbInitializer
    {
        Task Seed(IServiceProvider serviceProvider);
    }
}
