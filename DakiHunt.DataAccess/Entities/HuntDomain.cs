using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DakiHunt.DataAccess.Entities
{
    public class HuntDomain
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public Uri DomainUri { get; set; }

        public ICollection<DakiItem> Items { get; set; }

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
