using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    /// <summary>
    /// Represents a <see cref="WatchoutFeedback"/> name.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class FeedbackNameAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the feedback name.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackNameAttribute"/> class.
        /// </summary>
        /// <param name="name">The feedback name.</param>
        public FeedbackNameAttribute(String name)
        {
            Name = name;
        }
    }
}
