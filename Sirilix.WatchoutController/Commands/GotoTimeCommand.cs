using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Jump to the specified time position along the timeline.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class GotoTimeCommand : WatchoutCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GotoTimeCommand"/> class.
        /// </summary>
        public GotoTimeCommand()
            : base("gotoTime")
        {

        }

        /// <summary>
        /// Time position to go to.
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Name of auxiliary timeline to control (omit for main timeline).
        /// </summary>
        public String AuxiliaryTimelineName { get; set; }
    }
}
