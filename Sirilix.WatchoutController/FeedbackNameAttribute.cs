using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    public class FeedbackNameAttribute : Attribute
    {
        public String Name { get; set; }

        public FeedbackNameAttribute(String name)
        {
            Name = name;
        }
    }
}
