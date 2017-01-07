using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Feedbacks
{
    /// <summary>
    /// Sent repeatedly while busy doing lengthy tasks, such as downloading or
    //caching files.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutFeedback" />
    [FeedbackName("Busy")]
    public class BusyFeedback : WatchoutFeedback
    {
        /// <summary>
        /// What is being done (for instance, “Transferring”). May be empty string.
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// The subject of the above action (for instance, a file name). May be empty.
        /// </summary>
        public String Subject { get; set; }

        /// <summary>
        /// Percentage done so far, 0...100.
        /// </summary>
        public double Precentage { get; set; }
    }
}
