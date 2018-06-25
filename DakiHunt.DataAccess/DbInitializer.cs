using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Database;
using DakiHunt.DataAccess.Entities.Auth;
using DakiHunt.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DakiHunt.DataAccess
{
    public class DbInitializer : IDbInitializer
    {
        public async Task Seed(IServiceProvider serviceProvider)
        {
            var dakiDbContext = serviceProvider.GetRequiredService<DakiDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AuthUser>>();

#if DEBUG
            await userManager.CreateAsync(new AuthUser
            {
                Email = "a@a.com",
                EmailConfirmed = true,
                UserName = AuthUser.DebugUsername,
                DebugUser = true,
                RefreshToken = AuthUser.DebugRefreshToken,
            }, "lollol");
#endif

        }
    }
}
