using Sirilix.WatchoutController.Commands;
using Sirilix.WatchoutController.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    public class WatchoutDisplayClient : WatchoutClient, IWatchoutDisplayClient
    {
        public WatchoutDisplayClient()
            : base(3039)
        {

        }

        public async Task<ReadyFeedback> Authenticate()
        {
            return await SendCommand<ReadyFeedback>(new AuthenticateCommand());
        }

        public async Task<ReadyFeedback> Ping()
        {
            return await SendCommand<ReadyFeedback>(new PingCommand());
        }

        public async Task<ReplyFeedback> GetStatus()
        {
            return await SendCommand<ReplyFeedback>(new GetStatusCommand());
        }

        public async Task<ReadyFeedback> Load(String showName)
        {
            return await SendCommand<ReadyFeedback>(new LoadShowCommand() { ShowName = showName });
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

        public async Task<WatchoutFeedback> GotoTime(TimeSpan time, string auxiliaryTimelineName = null)
        {
            return await SendCommand<WatchoutFeedback>(new GotoTimeCommand() { Time = time, AuxiliaryTimelineName = auxiliaryTimelineName });
        }

        public async Task<WatchoutFeedback> GotoControlCue(string controlCueName, bool? searchInReverse = null, string auxiliaryTimelineName = null)
        {
            return await SendCommand<WatchoutFeedback>(new GotoControlCueCommand() { ControlCueName = controlCueName, SearchInReverse = searchInReverse, AuxiliaryTimelineName = auxiliaryTimelineName });
        }

        public async Task<WatchoutFeedback> StandBy(bool standBy, uint? fadeRate)
        {
            return await SendCommand<WatchoutFeedback>(new StandByCommand() { StandBy = standBy, FadeRate = fadeRate });
        }

        public async Task<WatchoutFeedback> SetInput(string inputName, double value, uint? fadeRate = null)
        {
            return await SendCommand<WatchoutFeedback>(new SetInputCommand() { InputName = inputName, Value = value, FadeRate = fadeRate });
        }

        public async Task<WatchoutFeedback> Reset()
        {
            return await SendCommand<WatchoutFeedback>(new ResetCommand());
        }
    }
}
