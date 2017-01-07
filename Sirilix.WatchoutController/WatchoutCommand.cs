using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    public class WatchoutCommand : IWatchoutCommand
    {
        private String _id;
        private String _name;

        private WatchoutCommand()
        {
            _id = Guid.NewGuid().ToString();
            _name = "ping";
        }

        public WatchoutCommand(String name)
            : this()
        {
            _name = name;
        }

        [WatchoutIgnore]
        public string ID
        {
            get
            {
                return _id;
            }
        }

        [WatchoutIgnore]
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public override string ToString()
        {
            String str = String.Empty;

            foreach (var prop in this.GetType().GetProperties())
            {
                str += prop.Name + ": " + prop.GetValue(this).ToString() + Environment.NewLine;
            }

            return str;
        }

        public String Serialize()
        {
            return CommandFormatter.Serialize(this);
        }
    }
}
