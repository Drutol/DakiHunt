using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.Models.Enums;

namespace DakiHunt.Models.Dtos
{
    public class UserPropertiesDto
    {
        public AccountType AccountType { get; set; }
        public int MaxHunts { get; set; }
        public int MinHuntUpdateTimeInSeconds { get; set; }
    }
}
