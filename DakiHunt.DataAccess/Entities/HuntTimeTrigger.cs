using System;
using System.Collections.Generic;
using System.Text;

namespace DakiHunt.DataAccess.Entities
{
    public class HuntTimeTrigger
    {
        public enum Type
        {
            Normal,
            Fast,
            VeryFast
        }

        public long Id { get; set; }
        public Type TriggerType { get; set; }
        /// <summary>
        /// Interval in minutes.
        /// </summary>
        public int Interval { get; set; }

        public IEnumerable<Hunt> Hunts { get; set; }
    }
}
