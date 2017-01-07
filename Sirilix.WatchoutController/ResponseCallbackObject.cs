using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    public class ResponseCallbackObject
    {
        public Delegate CallBack { get; set; }
        public Guid ID { get; set; }
    }
}
