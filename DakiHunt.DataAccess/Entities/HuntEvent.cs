using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.Models.Enums;

namespace DakiHunt.DataAccess.Entities
{
    public class HuntEvent   
    {
        public long Id { get; set; }

        public HuntEventType Type { get; set; }
        public string Message { get; set; }

        public Hunt Hunt { get; set; }
    }
}
