using Sirilix.WatchoutController.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sirilix.WatchoutController
{
    /// <summary>
    /// Represents the base interface for all clients.
    /// </summary>
    public interface IWatchoutClient
    {
        /// <summary>
        /// Occurs when a feedback has been received from the Watchout server.
        /// </summary>
        event EventHandler<WatchoutFeedback> FeedbackReceived;

        /// <summary>
        /// Occurs when an error has been received from the Watchout server.
        /// </summary>
        event EventHandler<ErrorFeedback> ErrorReceived;

        /// <summary>
        /// Connects the client to the specified Watchout computer.
        /// </summary>
        /// <param name="host">The host IP address.</param>
        void Connect(String host);

        /// <summary>
        /// Disconnects from the Watchout server.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Sends a string command.
        /// </summary>
        /// <param name="command">The command.</param>
        void SendCommand(String command);

        /// <summary>
        /// Sends the specified <see cref="WatchoutCommand"/>.
        /// </summary>
        /// <param name="command">The command.</param>
        void SendCommand(WatchoutCommand command);

        /// <summary>
        /// Sends the specified <see cref="WatchoutCommand"/> asynchronously while waiting for a response.
        /// </summary>
        /// <typeparam name="T">The <see cref="WatchoutFeedback"/> type.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns>Returns the proper feedback.</returns>
        Task<T> SendCommand<T>(WatchoutCommand command) where T : WatchoutFeedback;
    }
}
