using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.TestApp
{
    [Serializable]
    public class SettingsModel : ModelBase
    {

        private ObservableCollection<TimelineModel> _timelines;
        /// <summary>
        /// Gets or sets the Timelines.
        /// </summary>
        public ObservableCollection<TimelineModel> Timelines
        {
            get { return _timelines; }
            set { _timelines = value; RaisePropertyChanged("Timelines"); }
        }

        private String _watchoutServerAddress;
        /// <summary>
        /// Gets or sets the Watchout server address.
        /// </summary>
        public String WatchoutServerAddress
        {
            get { return _watchoutServerAddress; }
            set { _watchoutServerAddress = value; RaisePropertyChanged("WatchoutServerAddress"); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsModel"/> class.
        /// </summary>
        public SettingsModel()
        {
            Timelines = new ObservableCollection<TimelineModel>();
            WatchoutServerAddress = "127.0.0.1";
        }
    }
}
