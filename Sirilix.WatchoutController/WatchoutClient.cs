using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Sirilix.WatchoutController.Feedbacks;
using Sirilix.WatchoutController.Commands;

namespace Sirilix.WatchoutController
{
    /// <summary>
    /// Represents an abstract base class for all Watchout clients.
    /// </summary>
    /// <seealso cref="Sirilix.WatchoutController.IWatchoutClient" />
    public abstract class WatchoutClient : IWatchoutClient
    {
        private Thread receivingThread; //Separate thread for receiving feedbacks from the Watchout server.
        private Thread sendingThread; //Separate thread for sending commands.
        private TcpClient _tcpClient; //Encapsulated TCP client.
        private ConcurrentQueue<WatchoutCommand> _commandQueue; //Thread safe queue for holding commands to be sent.
        private List<WatchoutFeedback> _availableFeedbacks; //Contains the available feedbacks received from the Watchout server.
        private int _port; //The Watchout server port.

        #region Properties

        /// <summary>
        /// Gets the Watchout server host address.
        /// </summary>
        /// <value>
        /// The host address.
        /// </value>
        public String Host { get; private set; }

        /// <summary>
        /// Gets the Watchout server port.
        /// </summary>
        /// <value>
        /// The server port.
        /// </value>
        public int Port { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this client is connected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this client is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Gets or sets the number of seconds until a async command will return with an error if no proper feedback had arrived (Default is 2 seconds). 
        /// </summary>
        public int CommandsTimeout { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WatchoutClient"/> class.
        /// </summary>
        /// <param name="port">The server port.</param>
        public WatchoutClient(int port)
        {
            _port = port;
            _availableFeedbacks = new List<WatchoutFeedback>();
            _commandQueue = new ConcurrentQueue<WatchoutCommand>();
            CommandsTimeout = 2;
        }

        #endregion

        #region Threads Methods

        /// <summary>
        /// Sending thread loop.
        /// </summary>
        private void SendingMethod()
        {
            while (IsConnected)
            {
                if (_commandQueue.Count > 0)
                {
                    WatchoutCommand command = null;

                    if (_commandQueue.TryDequeue(out command))
                    {
                        String commandText = CommandFormatter.Serialize(command);
                        Debug.WriteLine("Sending: " + commandText);

                        try
                        {
                            var stream = _tcpClient.GetStream();
                            byte[] commandBytes = Encoding.UTF8.GetBytes(commandText);
                            stream.Write(commandBytes, 0, commandBytes.Length);
                        }
                        catch { }
                    }
                }

                Thread.Sleep(30);
            }
        }

        /// <summary>
        /// Receiving thread loop.
        /// </summary>
        private void ReceivingMethod()
        {
            while (IsConnected)
            {
                if (_tcpClient.Available > 0)
                {
                    byte[] commandBytes = new byte[_tcpClient.Available];
                    var stream = _tcpClient.GetStream();
                    stream.Read(commandBytes, 0, commandBytes.Length);
                    String commandText = Encoding.UTF8.GetString(commandBytes);
                    Debug.WriteLine("Received: " + commandText);
                    OnFeedbackReceived(commandText);
                }

                Thread.Sleep(30);
            }
        }

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Called when a feedback has been received.
        /// </summary>
        /// <param name="response">The response string.</param>
        protected virtual void OnFeedbackReceived(String response)
        {
            var feedback = CommandFormatter.Deserialize(response);
            if (feedback != null)
            {
                if (feedback.ID != null)
                {
                    _availableFeedbacks.Add(feedback);
                }

                if (feedback.GetType() == typeof(ErrorFeedback))
                {
                    if (ErrorReceived != null) ErrorReceived(this, feedback as ErrorFeedback);
                }
                else
                {
                    if (FeedbackReceived != null) FeedbackReceived(this, feedback);
                }
            }
        }

        #endregion

        #region IWatchoutClient Members

        /// <summary>
        /// Occurs when a feedback has been received from the Watchout server.
        /// </summary>
        public event EventHandler<WatchoutFeedback> FeedbackReceived;

        /// <summary>
        /// Occurs when an error has been received from the Watchout server.
        /// </summary>
        public event EventHandler<ErrorFeedback> ErrorReceived;

        /// <summary>
        /// Connects the client to the specified Watchout computer.
        /// </summary>
        /// <param name="host">The host IP address.</param>
        public void Connect(String host)
        {
            if (IsConnected)
            {
                Disconnect();
            }

            Host = host;
            Port = _port;
            _tcpClient = new TcpClient();
            _tcpClient.Connect(Host, Port);
            IsConnected = true;
            _tcpClient.ReceiveBufferSize = 1024;
            _tcpClient.SendBufferSize = 1024;

            receivingThread = new Thread(ReceivingMethod);
            receivingThread.IsBackground = true;
            receivingThread.Start();

            sendingThread = new Thread(SendingMethod);
            sendingThread.IsBackground = true;
            sendingThread.Start();
        }

        /// <summary>
        /// Disconnects from the Watchout server.
        /// </summary>
        public void Disconnect()
        {
            if (!IsConnected)
            {
                return;
            }

            _commandQueue = new ConcurrentQueue<WatchoutCommand>();
            _availableFeedbacks = new List<WatchoutFeedback>();
            Thread.Sleep(1000);
            IsConnected = false;
            Thread.Sleep(200);
            _tcpClient.Client.Disconnect(false);
            _tcpClient.Close();
        }

        /// <summary>
        /// Sends a string command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void SendCommand(string command)
        {
            Debug.WriteLine("Sending: " + command);
            var stream = _tcpClient.GetStream();
            byte[] commandBytes = Encoding.UTF8.GetBytes(command + "\r\n");
            stream.Write(commandBytes, 0, commandBytes.Length);
        }

        /// <summary>
        /// Sends the specified <see cref="WatchoutCommand" />.
        /// </summary>
        /// <param name="command">The command.</param>
        public void SendCommand(WatchoutCommand command)
        {
            _commandQueue.Enqueue(command);
        }

        /// <summary>
        /// Sends the specified <see cref="WatchoutCommand" /> asynchronously while waiting for a response.
        /// </summary>
        /// <typeparam name="T">The <see cref="WatchoutFeedback" /> type.</typeparam>
        /// <param name="command">The command.</param>
        /// <returns>
        /// Returns the proper feedback.
        /// </returns>
        public async Task<T> SendCommand<T>(WatchoutCommand command) where T : WatchoutFeedback
        {
            Task<T> task = new Task<T>(() =>
            {
                SendCommand(command);

                DateTime sentDate = DateTime.Now;

                while (!_availableFeedbacks.Exists(x => x.ID == command.ID) && DateTime.Now < sentDate.AddSeconds(CommandsTimeout))
                {
                    Thread.Sleep(30);
                }

                var feedback = _availableFeedbacks.SingleOrDefault(x => x.ID == command.ID) as T;

                if (feedback == null)
                {
                    WatchoutFeedback falseInstnce = Activator.CreateInstance<T>();
                    falseInstnce.CommandName = command.Name;
                    falseInstnce.ID = command.ID;
                    falseInstnce.Error = new ErrorFeedback() { ErrorKind = ErrorKind.NETWORK_ERRORS, Explanation = "Could not contact the Watchout server." };
                    return falseInstnce as T;
                }
                else
                {
                    feedback.CommandName = command.Name;
                }

                _availableFeedbacks.Remove(feedback);

                if (feedback.GetType() == typeof(ErrorFeedback))
                {
                    WatchoutFeedback falseInstnce = Activator.CreateInstance<T>();
                    falseInstnce.ID = feedback.ID;
                    falseInstnce.Error = feedback as ErrorFeedback;
                    return falseInstnce as T;
                }

                return feedback;
            });

            task.Start();
            await task;
            return task.Result;
        }

        #endregion
    }
}
