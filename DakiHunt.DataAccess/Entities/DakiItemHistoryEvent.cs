using System;
using System.Collections.Generic;
using System.Text;

namespace DakiHunt.DataAccess.Entities
{
    public class DakiItemHistoryEvent
    {
        public long Id { get; set; }

        public DateTime DateTime { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }

        public int PriceDiff { get; set; }
        public bool AvailabilityChanged { get; set; }

        /// <summary>
        /// Checks if item state differs, if it does returns true.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool DiffWithPrevious(DakiItemHistoryEvent other)
        {
            if (Price == other.Price && IsAvailable == other.IsAvailable)
                return false;

            PriceDiff = other.Price - Price;
            AvailabilityChanged = IsAvailable != other.IsAvailable;

            return true;
        }

        public DakiItem Item { get; set; }
    }
}
