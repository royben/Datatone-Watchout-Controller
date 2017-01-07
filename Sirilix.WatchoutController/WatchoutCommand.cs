using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    /// <summary>
    /// Represents a Watchout Command.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.IWatchoutCommand" />
    public class WatchoutCommand : IWatchoutCommand
    {
        private String _id;
        private String _name;

        /// <summary>
        /// Prevents a default instance of the <see cref="WatchoutCommand"/> class from being created.
        /// </summary>
        private WatchoutCommand()
        {
            _id = Guid.NewGuid().ToString();
            _name = "ping";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WatchoutCommand"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public WatchoutCommand(String name)
            : this()
        {
            _name = name;
        }

        /// <summary>
        /// Gets the Command unique identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [WatchoutIgnore]
        public string ID
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Gets the Command name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [WatchoutIgnore]
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            String str = String.Empty;

            foreach (var prop in this.GetType().GetProperties())
            {
                str += prop.Name + ": " + prop.GetValue(this).ToString() + Environment.NewLine;
            }

            return str;
        }

        /// <summary>
        /// Serializes this command.
        /// </summary>
        /// <returns>A string representation of this command.</returns>
        public String Serialize()
        {
            return CommandFormatter.Serialize(this);
        }
    }
}
