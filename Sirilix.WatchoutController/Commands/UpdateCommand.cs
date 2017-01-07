using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Update the display computers.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class UpdateCommand : WatchoutCommand
    {
        public UpdateCommand()
            : base("update")
        {

        }
    }
}
