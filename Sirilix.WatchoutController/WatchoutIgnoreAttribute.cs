using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    /// <summary>
    /// Represents a property attribute for decorating a <see cref="WatchoutCommand"/> properties to be ignored during serialization/deserialization.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class WatchoutIgnoreAttribute : Attribute
    {
    }
}
