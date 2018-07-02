using System;
using System.Collections.Generic;
using System.Text;

namespace DakiHunt.DataAccess.Models
{
    public class DakiItemSearchResultEntry
    {
        public string Identifier { get; set; }
        public bool IsAvailable { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public int PriceDiff { get; set; }
    }
}
