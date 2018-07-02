using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.Models.Enums;

namespace DakiHunt.DataAccess.Entities
{
    public class AccountProperties
    {
        public long Id { get; set; }

        /// <summary>
        /// Name of property set.
        /// </summary>
        public string PropertySetName { get; set; }
        /// <summary>
        /// Type of account.
        /// </summary>
        public AccountType AccountType { get; set; }
        /// <summary>
        /// How many hunts given user can have.
        /// </summary>
        public int MaxHunts { get; set; }
        /// <summary>
        /// How often can user schedule his hunts to update in seconds.
        /// </summary>
        public int MinHuntUpdateTimeInSeconds { get; set; }


        public ICollection<AppUser> Users { get; set; }
    }
}
