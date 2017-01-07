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
        /// <summary>
        /// Initializes a new instance of the <see cref="ResetCommand"/> class.
        /// </summary>
        public ResetCommand()
            : base("reset")
        {

        }
    }
}
