using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Jump to the time of specified Control cue. If the optional “reverse only” boolean
    /// is set to true, it searches for the Control cue only back in time from the current
    /// time position. Otherwise it searches first forward then reverse.
    /// The command does not change the run mode of the timeline. If specified cue is
    /// not found, the timeline’s state will not change, and a runtime error message to
    /// this effect will be returned.
    /// </summary>
    public class GotoControlCueCommand : WatchoutCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GotoControlCueCommand"/> class.
        /// </summary>
        public GotoControlCueCommand()
            : base("gotoControlCue")
        {
            SearchInReverse = true;
        }

        /// <summary>
        /// Name of Control cue to look for.
        /// </summary>
        public String ControlCueName { get; set; }

        /// <summary>
        /// Search in reverse only if true. If false or not specified, then search both ways.
        /// </summary>
        public bool? SearchInReverse { get; set; }

        /// <summary>
        /// Name of auxiliary timeline to control (omit for main timeline).
        /// </summary>
        public String AuxiliaryTimelineName { get; set; }
    }
}
