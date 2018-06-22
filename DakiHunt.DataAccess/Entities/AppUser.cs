using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.DataAccess.Interfaces.Service;
using DakiHunt.DataAccess.Interfaces.Service.Base;
using Microsoft.EntityFrameworkCore;

namespace DakiHunt.DataAccess.Entities
{
    public class AppUser : IModelWithRelation
    {
        public long Id { get; set; }

        public string AuthUserKey { get; set; }

        public ICollection<Hunt> Hunts { get; set; }


        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasMany(user => user.Hunts)
                .WithOne(hunt => hunt.User);
        }
    }
}
