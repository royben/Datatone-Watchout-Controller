using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    /// <summary>
    /// Represents an unrecognized Watchout feedback exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class UnrecognizedFeedbackException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnrecognizedFeedbackException"/> class.
        /// </summary>
        public UnrecognizedFeedbackException()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnrecognizedFeedbackException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnrecognizedFeedbackException(String message)
            : base(message)
        {

        }
    }
}
