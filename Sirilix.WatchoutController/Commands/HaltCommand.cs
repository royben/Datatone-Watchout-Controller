using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Stop at the current position, with optional auxiliary timeline name.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class HaltCommand : WatchoutCommand
    {
        public HaltCommand()
            : base("halt")
        {

        }

        /// <summary>
        /// Optional auxiliary timeline name.
        /// </summary>
        public String AuxiliaryTimelineName { get; set; }
    }
}
