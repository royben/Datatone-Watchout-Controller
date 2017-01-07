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
    public abstract class WatchoutClient : IWatchoutClient
    {
        private Thread receivingThread;
        private Thread sendingThread;
        private List<ResponseCallbackObject> callBacks;
        private TcpClient _tcpClient;
        private ConcurrentQueue<WatchoutCommand> _commandQueue;
        private List<WatchoutFeedback> _availableFeedbacks;
        private int _port;

        #region Properties

        public String Host { get; private set; }

        public int Port { get; private set; }

        public bool IsConnected { get; private set; }

        public int CommandsTimeout { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WatchoutClient"/> class.
        /// </summary>
        public WatchoutClient(int port)
        {
            _port = port;
            _availableFeedbacks = new List<WatchoutFeedback>();
            callBacks = new List<ResponseCallbackObject>();
            _commandQueue = new ConcurrentQueue<WatchoutCommand>();
            CommandsTimeout = 5;
        }

        #endregion

        #region Threads Methods

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

        protected virtual void OnFeedbackReceived(String command)
        {
            var feedback = CommandFormatter.Deserialize(command);
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

        #region Callback Methods

        //private void AddCallback(Delegate callBack, MessageBase msg)
        //{
        //    if (callBack != null)
        //    {
        //        Guid callbackID = Guid.NewGuid();
        //        ResponseCallbackObject responseCallback = new ResponseCallbackObject()
        //        {
        //            ID = callbackID,
        //            CallBack = callBack
        //        };

        //        msg.CallbackID = callbackID;
        //        callBacks.Add(responseCallback);
        //    }
        //}

        //private void InvokeMessageCallback(MessageBase msg, bool deleteCallback)
        //{
        //    var callBackObject = callBacks.SingleOrDefault(x => x.ID == msg.CallbackID);

        //    if (callBackObject != null)
        //    {
        //        if (deleteCallback)
        //        {
        //            callBacks.Remove(callBackObject);
        //        }
        //        callBackObject.CallBack.DynamicInvoke(this, msg);
        //    }
        //}

        #endregion

        #region IWatchoutClient Members

        public event EventHandler<WatchoutFeedback> FeedbackReceived;

        public event EventHandler<ErrorFeedback> ErrorReceived;

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

        public void Disconnect()
        {
            if (!IsConnected)
            {
                return;
            }

            _commandQueue = new ConcurrentQueue<WatchoutCommand>();
            _availableFeedbacks = new List<WatchoutFeedback>();
            callBacks.Clear();
            Thread.Sleep(1000);
            IsConnected = false;
            Thread.Sleep(200);
            _tcpClient.Client.Disconnect(false);
            _tcpClient.Close();
        }

        public void SendCommand(WatchoutCommand command)
        {
            _commandQueue.Enqueue(command);
        }

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

        public void SendCommand(string command)
        {
            Debug.WriteLine("Sending: " + command);
            var stream = _tcpClient.GetStream();
            byte[] commandBytes = Encoding.UTF8.GetBytes(command + "\r\n");
            stream.Write(commandBytes, 0, commandBytes.Length);
        }

        #endregion
    }
}
