using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Feedbacks
{
    /// <summary>
    /// Sent once when becomes ready after being busy (as indicated by one or more
    /// Busy messages). Also sent as response to the “ping” command. 
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutFeedback" />
    [FeedbackName("Ready")]
    public class ReadyFeedback : WatchoutFeedback
    {
        /// <summary>
        /// The version of the program.
        /// </summary>
        public String Version { get; set; }

        /// <summary>
        /// The name of the program.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The name of the computer/OS.
        /// </summary>
        public String OS { get; set; }

        /// <summary>
        /// License key is up to date.
        /// </summary>
        public bool IsLicensed { get; set; }
    }
}
