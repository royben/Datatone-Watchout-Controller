using Sirilix.WatchoutController.Commands;
using Sirilix.WatchoutController.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    /// <summary>
    /// Represents a Watchout Display Client.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.WatchoutClient" />
    /// <seealso cref="Sirilix.WatchoutController.IWatchoutDisplayClient" />
    public class WatchoutDisplayClient : WatchoutClient, IWatchoutDisplayClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WatchoutDisplayClient"/> class.
        /// </summary>
        public WatchoutDisplayClient()
            : base(3039)
        {

        }

        /// <summary>
        /// Before you can give any command (with the exception of the “ping”
        /// command), you must specify the authentication level. To control WATCHOUT,
        /// you need authentication level 1:
        /// </summary>
        /// <returns></returns>
        public async Task<ReadyFeedback> Authenticate()
        {
            return await SendCommand<ReadyFeedback>(new AuthenticateCommand());
        }

        /// <summary>
        /// Do-nothing command causing a Ready feedback message to be sent.
        /// </summary>
        /// <returns></returns>
        public async Task<ReadyFeedback> Ping()
        {
            return await SendCommand<ReadyFeedback>(new PingCommand());
        }

        /// <summary>
        /// Get the current status of the WATCHOUT cluster master.
        /// </summary>
        /// <returns></returns>
        public async Task<ReplyFeedback> GetStatus()
        {
            return await SendCommand<ReplyFeedback>(new GetStatusCommand());
        }

        /// <summary>
        /// Load a complete show specification from a local file associated with the show
        /// name specified by the first parameter. Busy feedback may be sent to the host
        /// while loading, informing the host about the progress (see “Busy” on page
        /// 270). If errors occur, Error feedback is sent (see “Error” on page 271). Finally,
        /// a Ready feedback message is sent, regardless of whether any error occurred
        /// (see “Ready” on page 269).
        /// </summary>
        /// <param name="showName">Name of the show.</param>
        /// <returns></returns>
        public async Task<ReadyFeedback> Load(String showName)
        {
            return await SendCommand<ReadyFeedback>(new LoadShowCommand() { ShowName = showName });
        }

        /// <summary>
        /// Run timeline from current position, optional aux timeline name.
        /// </summary>
        /// <param name="auxiliaryTimelineName">Name of the auxiliary timeline.</param>
        /// <returns></returns>
        public async Task<WatchoutFeedback> Run(string auxiliaryTimelineName = null)
        {
            return await SendCommand<WatchoutFeedback>(new RunCommand() { AuxiliaryTimelineName = auxiliaryTimelineName });
        }

        /// <summary>
        /// Stop at the current position, with optional auxiliary timeline name.
        /// </summary>
        /// <param name="auxiliaryTimelineName">Name of the auxiliary timeline.</param>
        /// <returns></returns>
        public async Task<WatchoutFeedback> Halt(string auxiliaryTimelineName = null)
        {
            return await SendCommand<WatchoutFeedback>(new HaltCommand() { AuxiliaryTimelineName = auxiliaryTimelineName });
        }

        /// <summary>
        /// Stop and deactivate the named auxiliary timeline.
        /// </summary>
        /// <param name="auxiliaryTimelineName">Name of the auxiliary timeline.</param>
        /// <returns></returns>
        public async Task<WatchoutFeedback> Kill(string auxiliaryTimelineName)
        {
            return await SendCommand<WatchoutFeedback>(new KillCommand() { AuxiliaryTimelineName = auxiliaryTimelineName });
        }

        /// <summary>
        /// Jump to the specified time position along the timeline.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="auxiliaryTimelineName">Name of the auxiliary timeline.</param>
        /// <returns></returns>
        public async Task<WatchoutFeedback> GotoTime(TimeSpan time, string auxiliaryTimelineName = null)
        {
            return await SendCommand<WatchoutFeedback>(new GotoTimeCommand() { Time = time, AuxiliaryTimelineName = auxiliaryTimelineName });
        }

        /// <summary>
        /// Jump to the time of specified Control cue. If the optional “reverse only” boolean
        /// is set to true, it searches for the Control cue only back in time from the current
        /// time position. Otherwise it searches first forward then reverse.
        /// The command does not change the run mode of the timeline. If specified cue is
        /// not found, the timeline’s state will not change, and a runtime error message to
        /// this effect will be returned.
        /// </summary>
        /// <param name="controlCueName">Name of the control cue.</param>
        /// <param name="searchInReverse">The search in reverse.</param>
        /// <param name="auxiliaryTimelineName">Name of the auxiliary timeline.</param>
        /// <returns></returns>
        public async Task<WatchoutFeedback> GotoControlCue(string controlCueName, bool? searchInReverse = null, string auxiliaryTimelineName = null)
        {
            return await SendCommand<WatchoutFeedback>(new GotoControlCueCommand() { ControlCueName = controlCueName, SearchInReverse = searchInReverse, AuxiliaryTimelineName = auxiliaryTimelineName });
        }

        /// <summary>
        /// Enter/exit standby mode. In standby, the display and sound is muted, or
        /// media on standby layers – if any – is performed (see “Perform Normal/In
        /// Standby” on page 104). This mode can be entered/exited smoothly, by specifying
        /// a fade rate
        /// </summary>
        /// <param name="standBy">if set to <c>true</c> [stand by].</param>
        /// <param name="fadeRate">The fade rate.</param>
        /// <returns></returns>
        public async Task<WatchoutFeedback> StandBy(bool standBy, uint? fadeRate)
        {
            return await SendCommand<WatchoutFeedback>(new StandByCommand() { StandBy = standBy, FadeRate = fadeRate });
        }

        /// <summary>
        /// Sets the value of a named input.
        /// The value is generally in the range 0 through 1, but may be extended to cover
        /// a wider range using the Limit setting of the Generic Input .
        /// </summary>
        /// <param name="inputName">Name of the input.</param>
        /// <param name="value">The value.</param>
        /// <param name="fadeRate">The fade rate.</param>
        /// <returns></returns>
        public async Task<WatchoutFeedback> SetInput(string inputName, double value, uint? fadeRate = null)
        {
            return await SendCommand<WatchoutFeedback>(new SetInputCommand() { InputName = inputName, Value = value, FadeRate = fadeRate });
        }

        /// <summary>
        /// Reset and stop all timelines.
        /// </summary>
        /// <returns></returns>
        public async Task<WatchoutFeedback> Reset()
        {
            return await SendCommand<WatchoutFeedback>(new ResetCommand());
        }
    }
}
