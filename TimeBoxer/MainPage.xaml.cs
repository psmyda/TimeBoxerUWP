using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TimeBoxer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        TimeSpan timerTime = new TimeSpan();
        DispatcherTimer timer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(360, 345));
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);

            UpdateLabels();

        }

        private void UpdateLabels()
        {
            screen.Text = timerTime.ToString();
        }

        private void Timer_Tick(object sender, object e)
        {
            timerTime -= TimeSpan.FromSeconds(1);
            UpdateLabels();
            if (timerTime == TimeSpan.Zero)
            {
                PlayAlarm();
                StopTime();
            }
        }

        private void StartTimer()
        {
            if (timerTime > TimeSpan.Zero)
            {
                startBtn.Visibility = Visibility.Collapsed;
                timer.Start();
            }
        }

        public void PlayAlarm()
        {
            mediaElement.Source = new Uri("ms-appx:///Assets/AlarmBeep.wav");
            mediaElement.Play();
        }

        public void AddTime(TimeSpan timeValue)
        {
            timerTime += timeValue;
            UpdateLabels();
        }

        public void SubstractTime(TimeSpan timeValue)
        {
            if (timerTime > timeValue)
            {
                timerTime -= timeValue;
            }
            else
            {
                StopTime();
            }

            UpdateLabels();
        }

        public void StopTime()
        {
            timer.Stop();
            timerTime = TimeSpan.Zero;
            startBtn.Visibility = Visibility.Visible;
            UpdateLabels();
        }

        private void controlBtn_Click(object sender, RoutedEventArgs e)
        {
            StartTimer();
        }

        private void manipulateBtn1_Click(object sender, RoutedEventArgs e)
        {
            SubstractTime(TimeSpan.FromMinutes(1));
        }

        private void manipulateBt2_Click(object sender, RoutedEventArgs e)
        {
            AddTime(TimeSpan.FromMinutes(1));
        }

        private void manipulateBtn3_Click(object sender, RoutedEventArgs e)
        {
            AddTime(TimeSpan.FromMinutes(2));
        }

        private void manipulateBtn4_Click(object sender, RoutedEventArgs e)
        {
            AddTime(TimeSpan.FromMinutes(5));
        }

        private void manipulateBtn5_Click(object sender, RoutedEventArgs e)
        {
            AddTime(TimeSpan.FromMinutes(15));
        }

        private void manipulateBtn6_Click(object sender, RoutedEventArgs e)
        {
            AddTime(TimeSpan.FromMinutes(30));
        }

        private void manipulateBtn7_Click(object sender, RoutedEventArgs e)
        {
            AddTime(TimeSpan.FromMinutes(60));
        }

        private void manipulateBtn8_Click(object sender, RoutedEventArgs e)
        {
            AddTime(TimeSpan.FromMinutes(120));
        }

        private void manipulateBtn9_Click(object sender, RoutedEventArgs e)
        {
            //AddTime(TimeSpan.FromMinutes(300));
            AddTime(TimeSpan.FromSeconds(5));
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            StopTime();
        }
    }
    
}
