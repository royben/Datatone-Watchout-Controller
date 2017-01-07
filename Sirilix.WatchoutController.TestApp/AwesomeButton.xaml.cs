using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sirilix.WatchoutController.TestApp
{
    /// <summary>
    /// Interaction logic for AwesomeButton.xaml
    /// </summary>
    public partial class AwesomeButton : Border
    {
        public AwesomeButton()
        {
            InitializeComponent();

            Stylus.SetIsPressAndHoldEnabled(this, false);
            Stylus.SetIsFlicksEnabled(this, false);

            this.MouseEnter += AwesomeButton_MouseEnter;
            this.MouseLeave += AwesomeButton_MouseLeave;
            this.PreviewMouseDown += AwesomeButton_PreviewMouseDown;
            this.PreviewMouseUp += AwesomeButton_PreviewMouseUp;
            this.TouchDown += AwesomeButton_TouchDown;
            this.TouchUp += AwesomeButton_TouchUp;

            this.Loaded += AwesomeButton_Loaded;
        }

        void AwesomeButton_Loaded(object sender, RoutedEventArgs e)
        {
            f.Foreground = NormalBrush;
        }

        void AwesomeButton_TouchUp(object sender, TouchEventArgs e)
        {
            f.Foreground = NormalBrush;
            AnimateUp();
        }

        void AwesomeButton_TouchDown(object sender, TouchEventArgs e)
        {
            f.Foreground = PressBrush;
            AnimateDown();
        }

        void AwesomeButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            f.Foreground = NormalBrush;

            if (Command != null)
            {
                Command.Execute(null);
            }

            AnimateUp();
        }

        void AwesomeButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            f.Foreground = PressBrush;
            AnimateDown();
        }

        void AwesomeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            f.Foreground = NormalBrush;
            AnimateUp();
        }

        void AwesomeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            f.Foreground = HoverBrush;
        }


        public Brush NormalBrush
        {
            get { return (Brush)GetValue(NormalBrushProperty); }
            set { SetValue(NormalBrushProperty, value); }
        }
        public static readonly DependencyProperty NormalBrushProperty =
            DependencyProperty.Register("NormalBrush", typeof(Brush), typeof(AwesomeButton), new PropertyMetadata(null));




        public Brush HoverBrush
        {
            get { return (Brush)GetValue(HoverBrushProperty); }
            set { SetValue(HoverBrushProperty, value); }
        }
        public static readonly DependencyProperty HoverBrushProperty =
            DependencyProperty.Register("HoverBrush", typeof(Brush), typeof(AwesomeButton), new PropertyMetadata(null));





        public Brush PressBrush
        {
            get { return (Brush)GetValue(PressBrushProperty); }
            set { SetValue(PressBrushProperty, value); }
        }
        public static readonly DependencyProperty PressBrushProperty =
            DependencyProperty.Register("PressBrush", typeof(Brush), typeof(AwesomeButton), new PropertyMetadata(null));






        public FontAwesomeIcon Image
        {
            get { return (FontAwesomeIcon)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(FontAwesomeIcon), typeof(AwesomeButton), new PropertyMetadata(FontAwesomeIcon.Adjust));





        public Brush CurrentBrush
        {
            get { return (Brush)GetValue(CurrentBrushProperty); }
            set { SetValue(CurrentBrushProperty, value); }
        }
        public static readonly DependencyProperty CurrentBrushProperty =
            DependencyProperty.Register("CurrentBrush", typeof(Brush), typeof(AwesomeButton), new PropertyMetadata(Brushes.White));





        public RelayCommand Command
        {
            get { return (RelayCommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(RelayCommand), typeof(AwesomeButton), new PropertyMetadata(null, CommandChanged));

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AwesomeButton).OnCommandChanged();
        }


        private void OnCommandChanged()
        {
            if (Command != null)
            {
                Command.CanExecuteChanged += Command_CanExecuteChanged;
            }
        }

        void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            IsEnabled = Command.CanExecute(null);
        }

        private void AnimateDown()
        {
            DoubleAnimation ani = new DoubleAnimation() { To = 0.95, Duration = new Duration(TimeSpan.FromSeconds(0.2)) };
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, ani);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, ani);
        }

        private void AnimateUp()
        {
            DoubleAnimation ani = new DoubleAnimation() { To = 1, Duration = new Duration(TimeSpan.FromSeconds(0.2)) };
            scale.BeginAnimation(ScaleTransform.ScaleXProperty, ani);
            scale.BeginAnimation(ScaleTransform.ScaleYProperty, ani);
        }

    }
}
