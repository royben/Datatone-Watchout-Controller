
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    /// <summary>
    /// Represents a Watchout Feedback/Response.
    /// </summary>
    public interface IWatchoutFeedback
    {
        /// <summary>
        /// Gets or sets the matching <see cref="WatchoutCommand"/> unique identifier.
        /// </summary>
        String ID { get; set; }
    }
}
