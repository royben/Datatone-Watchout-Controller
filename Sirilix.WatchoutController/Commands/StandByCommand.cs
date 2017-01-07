using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Enter/exit standby mode. In standby, the display and sound is muted, or
    ///media on standby layers – if any – is performed (see “Perform Normal/In
    ///Standby” on page 104). This mode can be entered/exited smoothly, by specifying
    ///a fade rate
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class StandByCommand : WatchoutCommand
    {
        public StandByCommand()
            : base("standBy")
        {
            StandBy = true;
            FadeRate = 0;
        }

        /// <summary>
        /// Enter standby if true, exit if false.
        /// </summary>
        public bool StandBy { get; set; }

        /// <summary>
        /// Fade rate, in milliseconds. Defaults to zero if not specified.
        /// </summary>
        public uint? FadeRate { get; set; }
    }
}
