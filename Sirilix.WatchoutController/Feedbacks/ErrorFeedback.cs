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
    // other reason.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutFeedback" />
    [FeedbackName("Error")]
    public class ErrorFeedback : WatchoutFeedback
    {
        public ErrorKind ErrorKind { get; set; }

        public String ErrorNumber { get; set; }

        public String Explanation { get; set; }

        [WatchoutIgnore]
        public String Description
        {
            get { return ErrorKind.ToDescription(); }
        }

        [WatchoutIgnore] //Hide Error from Error... :/
        [EditorBrowsable(EditorBrowsableState.Never)]
        private new ErrorFeedback Error { get; set; }

        public override string ToString()
        {
            return String.Format("{1}Command Name: {3}{1}Error Kind: {0}{1}Explanation: {2}", ErrorKind.ToDescription(), Environment.NewLine, Explanation,CommandName);
        }
    }
}
