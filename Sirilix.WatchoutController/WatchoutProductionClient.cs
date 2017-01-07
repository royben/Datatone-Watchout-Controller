using Sirilix.WatchoutController.Commands;
using Sirilix.WatchoutController.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    public class WatchoutProductionClient : WatchoutClient, IWatchoutProductionClient
    {
        public WatchoutProductionClient()
            : base(3040)
        {

        }

        public async Task<ReadyFeedback> Ping()
        {
            return await SendCommand<ReadyFeedback>(new PingCommand());
        }

        public async Task<WatchoutFeedback> Run(string auxiliaryTimelineName = null)
        {
            return await SendCommand<WatchoutFeedback>(new RunCommand() { AuxiliaryTimelineName = auxiliaryTimelineName });
        }

        public async Task<WatchoutFeedback> Halt(string auxiliaryTimelineName = null)
        {
            return await SendCommand<WatchoutFeedback>(new HaltCommand() { AuxiliaryTimelineName = auxiliaryTimelineName });
        }

        public async Task<WatchoutFeedback> Kill(string auxiliaryTimelineName)
        {
            return await SendCommand<WatchoutFeedback>(new KillCommand() { AuxiliaryTimelineName = auxiliaryTimelineName });
        }

        public async Task<WatchoutFeedback> Load(String showName)
        {
            return await SendCommand<WatchoutFeedback>(new LoadShowCommand() { ShowName = showName });
        }

        public async Task<WatchoutFeedback> GotoTime(TimeSpan time, string auxiliaryTimelineName = null)
        {
            return await SendCommand<WatchoutFeedback>(new GotoTimeCommand() { Time = time, AuxiliaryTimelineName = auxiliaryTimelineName });
        }

        public async Task<WatchoutFeedback> GotoControlCue(string controlCueName, bool? searchInReverse = null, string auxiliaryTimelineName = null)
        {
            return await SendCommand<WatchoutFeedback>(new GotoControlCueCommand() { ControlCueName = controlCueName, SearchInReverse = searchInReverse, AuxiliaryTimelineName = auxiliaryTimelineName });
        }

        public async Task<WatchoutFeedback> StandBy(bool standBy)
        {
            return await SendCommand<WatchoutFeedback>(new StandByCommand() { StandBy = standBy });
        }

        public async Task<WatchoutFeedback> Online(bool online)
        {
            return await SendCommand<WatchoutFeedback>(new OnlineCommand() { Online = online });
        }

        public async Task<WatchoutFeedback> Update()
        {
            return await SendCommand<WatchoutFeedback>(new UpdateCommand());
        }

        public async Task<WatchoutFeedback> SetInput(string inputName, double value, uint? fadeRate = null)
        {
            return await SendCommand<WatchoutFeedback>(new SetInputCommand() { InputName = inputName, Value = value, FadeRate = fadeRate });
        }
    }
}
