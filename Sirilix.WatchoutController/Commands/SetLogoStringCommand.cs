using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Display the string parameter next to the WATCHOUT logo, when shown on
    /// screen.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class SetLogoStringCommand : WatchoutCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetLogoStringCommand"/> class.
        /// </summary>
        public SetLogoStringCommand()
            : base("setLogoString")
        {

        }

        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        public String Text { get; set; }
    }
}
