using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Reset and stop all timelines.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class ResetCommand : WatchoutCommand
    {
        public ResetCommand()
            : base("reset")
        {

        }
    }
}
