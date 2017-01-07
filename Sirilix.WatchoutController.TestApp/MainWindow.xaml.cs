using Sirilix.WatchoutController.TestApp;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using Sirilix.WatchoutController;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sirilix.WatchoutController.TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private WatchoutProductionClient controller;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Settings = SettingsManager.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\settings.bin");

            InitializeCommands();

            controller = new WatchoutProductionClient();
            controller.CommandsTimeout = 2;

            this.Closing += MainWindow_Closing;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the Closing event of the MainWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsConnected)
            {
                ToggleConnection();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes the relay commands.
        /// </summary>
        private void InitializeCommands()
        {
            //Initialize Commands
            CloseCommand = new RelayCommand(() => { this.Close(); });

            AddNewTimelinesItemCommand = new RelayCommand(() => AddNewTimelinesItem());
            RemoveSelectedTimelinesItemCommand = new RelayCommand(() => RemoveSelectedTimelinesItem());
            StartTimelineCommand = new RelayCommand(() => StartTimeline(), () => { return IsConnected && !IsSendingCommand; });
            StopTimelineCommand = new RelayCommand(() => StopTimeline(), () => { return IsConnected && !IsSendingCommand; });
            ResetWatchoutCommand = new RelayCommand(() => ResetWatchout(), () => { return IsConnected && !IsSendingCommand; });
            PauseTimelineCommand = new RelayCommand(() => PauseTimeline(), () => { return IsConnected && !IsSendingCommand; });
            ToggleConnectionCommand = new RelayCommand(() => ToggleConnection());
            SaveCommand = new RelayCommand(() => SaveSettings());
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        private void SaveSettings()
        {
            SettingsManager.Save(Settings, System.AppDomain.CurrentDomain.BaseDirectory + "\\settings.bin");
        }

        /// <summary>
        /// Toggles the connection.
        /// </summary>
        private void ToggleConnection()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            if (!IsConnected)
            {
                try
                {
                    controller.Connect(Settings.WatchoutServerAddress);
                    IsConnected = true;
                }
                catch (Exception e)
                {
                    ShowErrorMessage(e.Message.ToString());
                }
            }
            else
            {
                try
                {
                    IsConnected = false;
                    controller.Disconnect();
                }
                catch (Exception e)
                {
                    ShowErrorMessage(e.Message.ToString());
                }
            }

            SetTimelineCommands(false);

            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// Pauses the timeline.
        /// </summary>
        private async void PauseTimeline()
        {
            if (SelectedTabletButton != null)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SetTimelineCommands(true);
                var result = await controller.Halt(SelectedTabletButton.AuxiliaryTimelineName);
                AddLog(result.ToString());
                SetTimelineCommands(false);
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Resets the Watchout.
        /// </summary>
        private async void ResetWatchout()
        {
            Mouse.OverrideCursor = Cursors.Wait;

            SetTimelineCommands(true);

            foreach (var item in Settings.Timelines)
            {
                var result = await controller.Kill(item.AuxiliaryTimelineName);
                AddLog(result.ToString());
            }

            SetTimelineCommands(false);

            Mouse.OverrideCursor = null;
        }

        /// <summary>
        /// Stops the timeline.
        /// </summary>
        private async void StopTimeline()
        {
            if (SelectedTabletButton != null)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SetTimelineCommands(true);
                var result = await controller.Kill(SelectedTabletButton.AuxiliaryTimelineName);
                AddLog(result.ToString());
                SetTimelineCommands(false);
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Starts the timeline.
        /// </summary>
        private async void StartTimeline()
        {
            if (SelectedTabletButton != null)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SetTimelineCommands(true);
                var result = await controller.Run(SelectedTabletButton.AuxiliaryTimelineName);
                AddLog(result.ToString());
                SetTimelineCommands(false);
                Mouse.OverrideCursor = null;
            }
        }

        /// <summary>
        /// Removes the selected Timelines item.
        /// </summary>
        private void RemoveSelectedTimelinesItem()
        {
            if (SelectedTimelinesItem != null)
            {
                Settings.Timelines.Remove(SelectedTimelinesItem);
                SelectedTimelinesItem = null;
            }
        }

        /// <summary>
        /// Adds the new Timelines item.
        /// </summary>
        private void AddNewTimelinesItem()
        {
            TimelineModel item = new TimelineModel();
            item.Name = "Item " + (Settings.Timelines.Count + 1);
            item.AuxiliaryTimelineName = "Timeline " + (Settings.Timelines.Count + 1);
            Settings.Timelines.Add(item);
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void ShowErrorMessage(String message)
        {
            MessageBox.Show(message, "Watchout Controller Test App", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="log">The log.</param>
        private void AddLog(String log)
        {
            txtLog.Text += (log + Environment.NewLine + Environment.NewLine);
            txtLog.ScrollToEnd();
        }

        /// <summary>
        /// Sets the timeline commands.
        /// </summary>
        /// <param name="sending">if set to <c>true</c> [sending].</param>
        private void SetTimelineCommands(bool sending)
        {
            IsSendingCommand = sending;
            StartTimelineCommand.RaiseCanExecuteChanged();
            StopTimelineCommand.RaiseCanExecuteChanged();
            ResetWatchoutCommand.RaiseCanExecuteChanged();
            PauseTimelineCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected
        {
            get { return (bool)GetValue(IsConnectedProperty); }
            set { SetValue(IsConnectedProperty, value); }
        }
        public static readonly DependencyProperty IsConnectedProperty =
            DependencyProperty.Register("IsConnected", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sending command.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is sending command; otherwise, <c>false</c>.
        /// </value>
        public bool IsSendingCommand
        {
            get { return (bool)GetValue(IsSendingCommandProperty); }
            set { SetValue(IsSendingCommandProperty, value); }
        }
        public static readonly DependencyProperty IsSendingCommandProperty =
            DependencyProperty.Register("IsSendingCommand", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        public SettingsModel Settings
        {
            get { return (SettingsModel)GetValue(SettingsProperty); }
            set { SetValue(SettingsProperty, value); }
        }
        public static readonly DependencyProperty SettingsProperty =
            DependencyProperty.Register("Settings", typeof(SettingsModel), typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the selected scenario.
        /// </summary>
        public TimelineModel SelectedScenario
        {
            get { return (TimelineModel)GetValue(SelectedScenarioProperty); }
            set { SetValue(SelectedScenarioProperty, value); }
        }
        public static readonly DependencyProperty SelectedScenarioProperty =
            DependencyProperty.Register("SelectedScenario", typeof(TimelineModel), typeof(MainWindow), new PropertyMetadata(null, SelectedTabletButtonChanged));

        /// <summary>
        /// Gets or sets the selected Timelines item.
        /// </summary>
        public TimelineModel SelectedTimelinesItem
        {
            get { return (TimelineModel)GetValue(SelectedTimelinesItemProperty); }
            set { SetValue(SelectedTimelinesItemProperty, value); }
        }
        public static readonly DependencyProperty SelectedTimelinesItemProperty =
            DependencyProperty.Register("SelectedTimelinesItem", typeof(TimelineModel), typeof(MainWindow), new PropertyMetadata(null, SelectedTabletButtonChanged));
        private static void SelectedTabletButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MainWindow).SelectedTabletButton = e.NewValue as TimelineModel;
        }

        /// <summary>
        /// Gets or sets the selected tablet button.
        /// </summary>
        public TimelineModel SelectedTabletButton
        {
            get { return (TimelineModel)GetValue(SelectedTabletButtonProperty); }
            set { SetValue(SelectedTabletButtonProperty, value); }
        }
        public static readonly DependencyProperty SelectedTabletButtonProperty =
            DependencyProperty.Register("SelectedTabletButton", typeof(TimelineModel), typeof(MainWindow), new PropertyMetadata(null));

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the close command.
        /// </summary>
        public RelayCommand CloseCommand
        {
            get { return (RelayCommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }
        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(RelayCommand), typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the add new Timelines item command.
        /// </summary>
        public RelayCommand AddNewTimelinesItemCommand
        {
            get { return (RelayCommand)GetValue(AddNewTimelinesItemCommandProperty); }
            set { SetValue(AddNewTimelinesItemCommandProperty, value); }
        }
        public static readonly DependencyProperty AddNewTimelinesItemCommandProperty =
            DependencyProperty.Register("AddNewTimelinesItemCommand", typeof(RelayCommand), typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the remove selected Timelines item command.
        /// </summary>
        public RelayCommand RemoveSelectedTimelinesItemCommand
        {
            get { return (RelayCommand)GetValue(RemoveSelectedTimelinesItemCommandProperty); }
            set { SetValue(RemoveSelectedTimelinesItemCommandProperty, value); }
        }
        public static readonly DependencyProperty RemoveSelectedTimelinesItemCommandProperty =
            DependencyProperty.Register("RemoveSelectedTimelinesItemCommand", typeof(RelayCommand), typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the start timeline command.
        /// </summary>
        public RelayCommand StartTimelineCommand
        {
            get { return (RelayCommand)GetValue(StartTimelineCommandProperty); }
            set { SetValue(StartTimelineCommandProperty, value); }
        }
        public static readonly DependencyProperty StartTimelineCommandProperty =
            DependencyProperty.Register("StartTimelineCommand", typeof(RelayCommand), typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the stop timeline command.
        /// </summary>
        public RelayCommand StopTimelineCommand
        {
            get { return (RelayCommand)GetValue(StopTimelineCommandProperty); }
            set { SetValue(StopTimelineCommandProperty, value); }
        }
        public static readonly DependencyProperty StopTimelineCommandProperty =
            DependencyProperty.Register("StopTimelineCommand", typeof(RelayCommand), typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the reset watchout command.
        /// </summary>
        public RelayCommand ResetWatchoutCommand
        {
            get { return (RelayCommand)GetValue(ResetWatchoutCommandProperty); }
            set { SetValue(ResetWatchoutCommandProperty, value); }
        }
        public static readonly DependencyProperty ResetWatchoutCommandProperty =
            DependencyProperty.Register("ResetWatchoutCommand", typeof(RelayCommand), typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the pause timeline command.
        /// </summary>
        public RelayCommand PauseTimelineCommand
        {
            get { return (RelayCommand)GetValue(PauseTimelineCommandProperty); }
            set { SetValue(PauseTimelineCommandProperty, value); }
        }
        public static readonly DependencyProperty PauseTimelineCommandProperty =
            DependencyProperty.Register("PauseTimelineCommand", typeof(RelayCommand), typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the toggle connection command.
        /// </summary>
        public RelayCommand ToggleConnectionCommand
        {
            get { return (RelayCommand)GetValue(ToggleConnectionCommandProperty); }
            set { SetValue(ToggleConnectionCommandProperty, value); }
        }
        public static readonly DependencyProperty ToggleConnectionCommandProperty =
            DependencyProperty.Register("ToggleConnectionCommand", typeof(RelayCommand), typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the save command.
        /// </summary>
        public RelayCommand SaveCommand
        {
            get { return (RelayCommand)GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }
        public static readonly DependencyProperty SaveCommandProperty =
            DependencyProperty.Register("SaveCommand", typeof(RelayCommand), typeof(MainWindow), new PropertyMetadata(null));

        #endregion
    }
}
