using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    public class UnrecognizedFeedbackException : Exception
    {
        public UnrecognizedFeedbackException()
        {
            
        }

        public UnrecognizedFeedbackException(String message)
            : base(message)
        {

        }
    }
}
