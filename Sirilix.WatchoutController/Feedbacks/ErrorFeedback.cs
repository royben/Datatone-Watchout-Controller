using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Feedbacks
{
    /// <summary>
    /// Sent when any error occurs, either as a direct result of a command, or for any
    /// other reason.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutFeedback" />
    [FeedbackName("Error")]
    public class ErrorFeedback : WatchoutFeedback
    {
        /// <summary>
        /// Gets or sets the kind of the error.
        /// </summary>
        /// <value>
        /// The kind of the error.
        /// </value>
        public ErrorKind ErrorKind { get; set; }

        /// <summary>
        /// Gets or sets the error number.
        /// </summary>
        public String ErrorNumber { get; set; }

        /// <summary>
        /// Gets or sets the explanation for this error.
        /// </summary>
        public String Explanation { get; set; }

        /// <summary>
        /// Gets the error description.
        /// </summary>
        /// <value>
        /// The error description.
        /// </value>
        [WatchoutIgnore]
        public String Description
        {
            get { return ErrorKind.ToDescription(); }
        }

        /// <exclude/>
        [WatchoutIgnore] //Hide Error from Error... :/
        [EditorBrowsable(EditorBrowsableState.Never)]
        private new ErrorFeedback Error { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return String.Format("{1}Command Name: {3}{1}Error Kind: {0}{1}Explanation: {2}", ErrorKind.ToDescription(), Environment.NewLine, Explanation,CommandName);
        }
    }
}
