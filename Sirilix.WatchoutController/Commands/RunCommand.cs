using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Run timeline from current position, optional aux timeline name.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class RunCommand : WatchoutCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunCommand"/> class.
        /// </summary>
        public RunCommand()
            : base("run")
        {

        }

        /// <summary>
        /// Optional aux timeline name.
        /// </summary>
        public String AuxiliaryTimelineName { get; set; }
    }
}
