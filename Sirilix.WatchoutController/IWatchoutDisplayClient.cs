using Sirilix.WatchoutController.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    public interface IWatchoutDisplayClient
    {
        Task<ReadyFeedback> Authenticate();

        Task<ReadyFeedback> Ping();

        Task<ReplyFeedback> GetStatus();

        Task<ReadyFeedback> Load(String showName);

        Task<WatchoutFeedback> Run(String auxiliaryTimelineName = null);

        Task<WatchoutFeedback> Halt(String auxiliaryTimelineName = null);

        Task<WatchoutFeedback> Kill(String auxiliaryTimelineName);

        Task<WatchoutFeedback> GotoTime(TimeSpan time, String auxiliaryTimelineName = null);

        Task<WatchoutFeedback> GotoControlCue(String controlCueName, bool? searchInReverse = null, String auxiliaryTimelineName = null);

        Task<WatchoutFeedback> StandBy(bool standBy, uint? fadeRate = null);

        Task<WatchoutFeedback> Reset();

        Task<WatchoutFeedback> SetInput(String inputName, double value, uint? fadeRate = null);
    }
}
