using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.DataAccess.Interfaces.Service.Base;
using Microsoft.EntityFrameworkCore;

namespace DakiHunt.DataAccess.Entities
{
    public class Hunt : IModelWithRelation
    {
        public enum Type
        {
            SingleItem,
            Search
        }

        public long Id { get; set; }

        public AppUser User { get; set; }
        public DakiItem HuntedItem { get; set; }
        public HuntTimeTrigger TimeTrigger { get; set; }

        public bool IsActive { get; set; }
        public bool IsFinished { get; set; }

        public ICollection<HuntEvent> HistoryEvents { get; set; }

        public Type HuntType { get; set; }
        
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hunt>()
                .HasOne(hunt => hunt.TimeTrigger)
                .WithMany(trigger => trigger.Hunts);

            modelBuilder.Entity<Hunt>()
                .HasOne(hunt => hunt.HuntedItem)
                .WithMany(item => item.Hunts);

            modelBuilder.Entity<Hunt>()
                .HasMany(hunt => hunt.HistoryEvents)
                .WithOne(e => e.Hunt);
        }
    }
}
