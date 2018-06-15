using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.DataAccess.Entities.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DakiHunt.DataAccess.Database
{
    public class DakiDbContext : IdentityDbContext<AppUser>
    {
        public DakiDbContext(DbContextOptions<DakiDbContext> options) : base(options)
        {

        }
    }
}
