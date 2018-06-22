using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.DataAccess.Interfaces.Service.Base;
using Microsoft.EntityFrameworkCore;

namespace DakiHunt.DataAccess.Entities
{
    public class DakiItem : IModelWithRelation
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }

        public HuntDomain Domain { get; set; }

        public ICollection<Hunt> Hunts { get; set; }
        public ICollection<DakiItemHistoryEvent> HistoryEvents { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DakiItem>()
                .HasOne(item => item.Domain)
                .WithMany(domain => domain.Items);

            modelBuilder.Entity<DakiItem>()
                .HasMany(item => item.HistoryEvents)
                .WithOne(historyEvent => historyEvent.Item);
        }
    }
}
