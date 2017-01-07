using Sirilix.WatchoutController.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    public interface IWatchoutClient
    {
        event EventHandler<WatchoutFeedback> FeedbackReceived;

        event EventHandler<ErrorFeedback> ErrorReceived;

        void Connect(String host);

        void Disconnect();

        void SendCommand(String command);

        void SendCommand(WatchoutCommand command);

        Task<T> SendCommand<T>(WatchoutCommand command) where T : WatchoutFeedback;
    }
}
