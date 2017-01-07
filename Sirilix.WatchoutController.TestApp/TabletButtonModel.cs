using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Sirilix.WatchoutController.TestApp
{
    [Serializable]
    public class TimelineModel : ModelBase
    {
        private String _name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public String Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        private String _auxiliaryTimelineName;
        /// <summary>
        /// Gets or sets the name of the auxiliary timeline.
        /// </summary>
        public String AuxiliaryTimelineName
        {
            get { return _auxiliaryTimelineName; }
            set { _auxiliaryTimelineName = value; RaisePropertyChanged("AuxiliaryTimelineName"); }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name + " - " + AuxiliaryTimelineName;
        }
    }
}
