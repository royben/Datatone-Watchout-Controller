using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    /// <summary>
    /// Represents a Watchout Command.
    /// </summary>
    public interface IWatchoutCommand
    {
        /// <summary>
        /// Gets the Command unique identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [WatchoutIgnore]
        String ID { get; }

        /// <summary>
        /// Gets the Command name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [WatchoutIgnore]
        String Name { get; }
    }
}
