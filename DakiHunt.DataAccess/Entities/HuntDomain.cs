using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DakiHunt.DataAccess.Interfaces.Service.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DakiHunt.DataAccess.Entities
{
    public class HuntDomain : IModelWithRelation
    {
        public long Id { get; set; }

        public string Name { get; set; }  
        public Uri Uri { get; set; }

        public ICollection<DakiItem> Items { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DbConfiguration());
        }

        class DbConfiguration : IEntityTypeConfiguration<HuntDomain>
        {
            public void Configure(EntityTypeBuilder<HuntDomain> builder)
            {
                builder.Property(domain => domain.Uri).HasConversion(uri => uri.ToString(), s => new Uri(s));
            }
        }

        #region EqualityComparer

        private sealed class IdEqualityComparer : IEqualityComparer<HuntDomain>
        {
            public bool Equals(HuntDomain x, HuntDomain y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id;
            }

            public int GetHashCode(HuntDomain obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        [NotMapped]
        public static IEqualityComparer<HuntDomain> IdComparer { get; } = new IdEqualityComparer();

        #endregion
    }
}
