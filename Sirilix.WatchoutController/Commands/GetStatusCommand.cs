using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Get the current status of the WATCHOUT cluster master.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class GetStatusCommand : WatchoutCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetStatusCommand"/> class.
        /// </summary>
        public GetStatusCommand()
            : base("getStatus")
        {

        }
    }
}
