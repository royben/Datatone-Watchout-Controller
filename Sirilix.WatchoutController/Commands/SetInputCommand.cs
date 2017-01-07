using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Sets the value of a named input.
    /// The value is generally in the range 0 through 1, but may be extended to cover
    /// a wider range using the Limit setting of the Generic Input .
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class SetInputCommand : WatchoutCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetInputCommand"/> class.
        /// </summary>
        public SetInputCommand()
            : base("setInput")
        {

        }

        /// <summary>
        /// Gets or sets the name of the input.
        /// </summary>
        public String InputName { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Fade rate, in milliseconds. Defaults to zero if not specified.
        /// </summary>
        public uint? FadeRate { get; set; }
    }
}
