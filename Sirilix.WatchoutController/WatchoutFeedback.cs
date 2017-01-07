using Sirilix.WatchoutController.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    public class WatchoutFeedback : IWatchoutFeedback
    {
        [WatchoutIgnore]
        public String ID { get; set; }

        public String CommandName { get; set; }

        [WatchoutIgnore]
        public ErrorFeedback Error { get; set; }

        [WatchoutIgnore]
        public bool CommandSuccessful
        {
            get { return Error == null; }
        }

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
