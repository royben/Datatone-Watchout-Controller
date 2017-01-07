using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Control the online status of the production software.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class OnlineCommand : WatchoutCommand
    {
        public OnlineCommand()
            : base("online")
        {

        }

        /// <summary>
        /// Gets or sets a value indicating whether the production software is online.
        /// </summary>
        public bool Online { get; set; }
    }
}
