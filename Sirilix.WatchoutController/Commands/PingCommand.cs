using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Do-nothing command causing a Ready feedback message to be sent.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class PingCommand : WatchoutCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PingCommand"/> class.
        /// </summary>
        public PingCommand()
            : base("ping")
        {

        }
    }
}
