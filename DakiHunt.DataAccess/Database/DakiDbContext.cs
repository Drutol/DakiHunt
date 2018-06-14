using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DakiHunt.DataAccess.Database
{
    public class DakiDbContext : DbContext
    {
        public DakiDbContext(DbContextOptions<DakiDbContext> options) : base(options)
        {

        }
    }
}
