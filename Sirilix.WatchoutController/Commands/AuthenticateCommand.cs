using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Before you can give any command (with the exception of the “ping”
    ///command), you must specify the authentication level. To control WATCHOUT,
    ///you need authentication level 1:
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class AuthenticateCommand : WatchoutCommand
    {
        public AuthenticateCommand()
            : base("authenticate")
        {
            Level = 1;
        }

        /// <summary>
        /// Authentication Level.
        /// </summary>
        public int Level { get; set; }
    }
}
