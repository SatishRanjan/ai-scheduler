using System;
using System.Collections.Generic;
using System.Text;

namespace ai_scheduler.src
{
    public enum Operation
    {
        TRANSFORM,
        TRANSFER
    }

    public enum ResourceKind
    {
        Raw,
        Created,
        Waste
    }
}
