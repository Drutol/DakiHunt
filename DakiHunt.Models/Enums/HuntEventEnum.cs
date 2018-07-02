﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DakiHunt.Models.Enums
{
    public enum HuntEventType
    {
        Created = 100,
        Paused = 101,
        Resumed = 102,
        Updated = 200,
        Faulted = 300,
        Finished = 400,
    }
}
