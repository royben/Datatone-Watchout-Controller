using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    /// <summary>
    /// Represents the enumeration for the different Cluster Health statuses returned by the getStatus Command.
    /// </summary>
    public enum ClusterHealth
    {
        /// <summary>
        /// Status OK.
        /// </summary>
        OK = 0,
        /// <summary>
        /// Status is Suboptimal.
        /// </summary>
        Suboptimal = 1,
        /// <summary>
        /// Server has problems.
        /// </summary>
        Problems = 2,
        /// <summary>
        /// The server is dead.
        /// </summary>
        Dead = 3
    }
}
