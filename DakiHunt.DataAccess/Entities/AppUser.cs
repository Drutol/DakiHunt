using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.DataAccess.Interfaces.Service;
using DakiHunt.DataAccess.Interfaces.Service.Base;
using DakiHunt.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DakiHunt.DataAccess.Entities
{
    public class AppUser : IModelWithRelation
    {
        public long Id { get; set; }

        public string AuthUserId { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public AccountProperties AccountProperties { get; set; }

        public ICollection<Hunt> Hunts { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasOne(user => user.AccountProperties)
                .WithMany(properties => properties.Users);

            modelBuilder.Entity<AppUser>()
                .HasMany(user => user.Hunts)
                .WithOne(hunt => hunt.User);
        }
    }
}
