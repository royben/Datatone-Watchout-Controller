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
    /// Represents a Watchout Production Client.
    /// </summary>
    public interface IWatchoutProductionClient
    {
        /// <summary>
        /// Do-nothing command causing a Ready feedback message to be sent.
        /// </summary>
        /// <returns></returns>
        Task<ReadyFeedback> Ping();

        /// <summary>
        /// Run timeline from current position, optional aux timeline name.
        /// </summary>
        /// <param name="auxiliaryTimelineName">Name of the auxiliary timeline.</param>
        /// <returns></returns>
        Task<WatchoutFeedback> Run(String auxiliaryTimelineName = null);

        /// <summary>
        /// Stop at the current position, with optional auxiliary timeline name.
        /// </summary>
        /// <param name="auxiliaryTimelineName">Name of the auxiliary timeline.</param>
        /// <returns></returns>
        Task<WatchoutFeedback> Halt(String auxiliaryTimelineName = null);

        /// <summary>
        /// Stop and deactivate the named auxiliary timeline.
        /// </summary>
        /// <param name="auxiliaryTimelineName">Name of the auxiliary timeline.</param>
        /// <returns></returns>
        Task<WatchoutFeedback> Kill(String auxiliaryTimelineName);

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
        Task<WatchoutFeedback> Load(String showName);

        /// <summary>
        /// Jump to the specified time position along the timeline.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="auxiliaryTimelineName">Name of the auxiliary timeline.</param>
        /// <returns></returns>
        Task<WatchoutFeedback> GotoTime(TimeSpan time, String auxiliaryTimelineName = null);

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
        Task<WatchoutFeedback> GotoControlCue(String controlCueName, bool? searchInReverse = null, String auxiliaryTimelineName = null);

        /// <summary>
        /// Enter/exit standby mode. In standby, the display and sound is muted, or
        /// media on standby layers – if any – is performed (see “Perform Normal/In
        /// Standby” on page 104). This mode can be entered/exited smoothly, by specifying
        /// a fade rate
        /// </summary>
        /// <param name="standBy">if set to <c>true</c> [stand by].</param>
        /// <returns></returns>
        Task<WatchoutFeedback> StandBy(bool standBy);

        /// <summary>
        /// Control the online status of the production software.
        /// </summary>
        /// <param name="online">if set to <c>true</c> [online].</param>
        /// <returns></returns>
        Task<WatchoutFeedback> Online(bool online);

        /// <summary>
        /// Update the display computers.
        /// </summary>
        /// <returns></returns>
        Task<WatchoutFeedback> Update();

        /// <summary>
        /// Sets the value of a named input.
        /// The value is generally in the range 0 through 1, but may be extended to cover
        /// a wider range using the Limit setting of the Generic Input .
        /// </summary>
        /// <param name="inputName">Name of the input.</param>
        /// <param name="value">The value.</param>
        /// <param name="fadeRate">The fade rate.</param>
        /// <returns></returns>
        Task<WatchoutFeedback> SetInput(String inputName, double value, uint? fadeRate = null);
    }
}
