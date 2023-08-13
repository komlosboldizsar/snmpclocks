using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNMPclocks
{
    public enum ClockState
    {
        Time,
        Reset,
        Stopped,
        Running,
        Expired
    }
}
