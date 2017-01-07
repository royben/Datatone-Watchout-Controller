using Sirilix.WatchoutController.Feedbacks;
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
    /// <seealso cref="Sirilix.WatchoutController.IWatchoutFeedback" />
    public class WatchoutFeedback : IWatchoutFeedback
    {
        /// <summary>
        /// Gets or sets the matching <see cref="WatchoutCommand" /> unique identifier.
        /// </summary>
        [WatchoutIgnore]
        public String ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the command.
        /// </summary>
        /// <value>
        /// The name of the command.
        /// </value>
        public String CommandName { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ErrorFeedback"/> if there is any.
        /// </summary>
        [WatchoutIgnore]
        public ErrorFeedback Error { get; set; }

        /// <summary>
        /// Gets a value indicating whether to command was successful.
        /// </summary>
        [WatchoutIgnore]
        public bool CommandSuccessful
        {
            get { return Error == null; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this feedback.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this feedback.
        /// </returns>
        public override string ToString()
        {
            String str = String.Empty;

            foreach (var prop in this.GetType().GetProperties())
            {
                if (prop.GetValue(this) != null && prop.Name != "HasError")
                {
                    str += prop.Name + ": " + prop.GetValue(this).ToString() + Environment.NewLine;
                }
            }

            return str;
        }
    }
}
