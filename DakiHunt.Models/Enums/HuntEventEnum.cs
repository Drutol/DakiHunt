using System;
using System.Collections.Generic;
using System.Text;

namespace DakiHunt.Models.Enums
{
    public enum HuntEventType
    {
        Created = 100,
        Paused = 101,
        Resumed = 102,
        UpdatedWithStateChange = 200,
        UpdatedWithoutStateChange = 201,
        Faulted = 300,
        FaultedUpdate = 301,
        Finished = 400,
    }
}
