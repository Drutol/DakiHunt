using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.DataAccess.Entities.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DakiHunt.DataAccess.Database
{
    public class DakiAccDbContext : IdentityDbContext<AuthUser>
    {
        public DakiAccDbContext(DbContextOptions<DakiAccDbContext> options) : base(options)
        {

        }
    }
}
