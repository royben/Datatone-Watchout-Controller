using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    public interface IWatchoutCommand
    {
        [WatchoutIgnore]
        String ID { get; }
        [WatchoutIgnore]
        String Name { get; }
    }
}
