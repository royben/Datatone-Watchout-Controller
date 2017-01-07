using Sirilix.WatchoutController.Commands;
using Sirilix.WatchoutController.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    public interface IWatchoutProductionClient
    {
        Task<ReadyFeedback> Ping();

        Task<WatchoutFeedback> Run(String auxiliaryTimelineName = null);

        Task<WatchoutFeedback> Halt(String auxiliaryTimelineName = null);

        Task<WatchoutFeedback> Kill(String auxiliaryTimelineName);

        Task<WatchoutFeedback> Load(String showName);

        Task<WatchoutFeedback> GotoTime(TimeSpan time, String auxiliaryTimelineName = null);

        Task<WatchoutFeedback> GotoControlCue(String controlCueName, bool? searchInReverse = null, String auxiliaryTimelineName = null);

        Task<WatchoutFeedback> StandBy(bool standBy);

        Task<WatchoutFeedback> Online(bool online);

        Task<WatchoutFeedback> Update();

        Task<WatchoutFeedback> SetInput(String inputName, double value, uint? fadeRate = null);
    }
}
