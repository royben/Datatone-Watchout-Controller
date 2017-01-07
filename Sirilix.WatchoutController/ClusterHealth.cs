using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    public enum ClusterHealth
    {
        OK = 0,
        Suboptimal = 1,
        Problems = 2,
        Dead = 3
    }
}
