using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DakiHunt.DataAccess.Entities;
using DakiHunt.DataAccess.Interfaces.Service;
using DakiHunt.DataAccess.Interfaces.Service.Base;
using Microsoft.EntityFrameworkCore;

namespace DakiHunt.DataAccess.Database
{
    public class DakiDbContext : DbContext
    {
        public DakiDbContext(DbContextOptions<DakiDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Hunt> Hunts { get; set; }
        public DbSet<HuntDomain> HuntDomains { get; set; }
        public DbSet<HuntTimeTrigger> HuntTimeTriggers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var modelType in GetClassesFromNamespace())
            {
                modelType.GetMethod("OnModelCreating").Invoke(null, new object[] { modelBuilder });
            }

            base.OnModelCreating(modelBuilder);
        }

        private IEnumerable<Type> GetClassesFromNamespace()
        {
            return Assembly.GetAssembly(typeof(DakiDbContext))
                .GetTypes()
                .Where(t => t.IsClass && t.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IModelWithRelation)));
        }
    }
}
