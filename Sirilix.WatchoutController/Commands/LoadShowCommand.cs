using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Commands
{
    /// <summary>
    /// Load a complete show specification from a local file associated with the show
    /// name specified by the first parameter. Busy feedback may be sent to the host
    /// while loading, informing the host about the progress (see “Busy” on page
    /// 270). If errors occur, Error feedback is sent (see “Error” on page 271). Finally,
    /// a Ready feedback message is sent, regardless of whether any error occurred
    /// (see “Ready” on page 269).
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutCommand" />
    public class LoadShowCommand : WatchoutCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadShowCommand"/> class.
        /// </summary>
        public LoadShowCommand()
            : base("load")
        {

        }

        /// <summary>
        /// Name of the show to be loaded.
        /// NOTE: You can not specify a folder path to the show. The show must be
        /// present in the “Shows” folder, located in the same folder as the
        /// WATCHOUT display software.
        /// </summary>
        public String ShowName { get; set; }
    }
}
