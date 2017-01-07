using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Stop and deactivate the named auxiliary timeline.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class KillCommand : WatchoutCommand
    {
        public KillCommand()
            : base("kill")
        {

        }

        /// <summary>
        /// The named auxiliary timeline.
        /// </summary>
        public String AuxiliaryTimelineName { get; set; }
    }
}
