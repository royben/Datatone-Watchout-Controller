using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController.Feedbacks
{
    /// <summary>
    /// A Reply feedback message is sent as a direct response to a query command
    /// (for instance, getStatus described on page 265). Use a command ID to positively
    /// associate the reply with the command.
    /// The format of the reply parameter(s) depends on the command that caused the
    /// reply to be sent.
    /// </summary>
    [FeedbackName("Reply")]
    public class ReplyFeedback : WatchoutFeedback
    {
        /// <summary>
        /// Name of the show. Empty string if no show loaded.
        /// </summary>
        public String ShowName { get; set; }

        /// <summary>
        /// Busy. True if the master display computer or any of its slaves is busy 
        /// </summary>
        public bool IsBusy { get; set; }

        /// <summary>
        /// General health status of the cluster; 0: OK, 1: Suboptimal, 2: Problems, 3:
        /// Dead.
        /// </summary>
        public ClusterHealth ClusterHealth { get; set; }

        /// <summary>
        /// Display is open (in its full screen mode).
        /// </summary>
        public bool IsDisplayFullScreen { get; set; }

        /// <summary>
        /// Show is active (ready to run).
        /// </summary>
        public bool IsShowActive { get; set; }

        /// <summary>
        /// Programmer is on line.
        /// </summary>
        public bool IsProgrammerOnline { get; set; }

        /// <summary>
        /// Current time position, in milliseconds (only included if show is active).
        /// </summary>
        public TimeSpan CurrentPosition { get; set; }

        /// <summary>
        /// Show is playing – false if paused (only included if show is active).
        /// </summary>
        public bool IsShowPlaying { get; set; }

        /// <summary>
        /// Timeline rate (nominally 1, only included if show is active).
        /// </summary>
        public float TimelineRate { get; set; }

        /// <summary>
        /// Standby mode (true if in standby, only included if show is active)
        /// </summary>
        public bool IsStandBy { get; set; }
    }
}
