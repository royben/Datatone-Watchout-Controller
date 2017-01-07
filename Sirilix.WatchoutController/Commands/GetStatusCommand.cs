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
        public GetStatusCommand()
            : base("getStatus")
        {

        }
    }
}
