using Microsoft.Toolkit.Uwp.Notifications; // Notifications library
using System;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TimeBoxer
{
    using Windows.UI.Notifications;

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
            SendToast();
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
            AddTime(TimeSpan.FromMinutes(25));
        }

        private void manipulateBtn7_Click(object sender, RoutedEventArgs e)
        {
            AddTime(TimeSpan.FromMinutes(30));
        }

        private void manipulateBtn8_Click(object sender, RoutedEventArgs e)
        {
            AddTime(TimeSpan.FromMinutes(60));
        }

        private void manipulateBtn9_Click(object sender, RoutedEventArgs e)
        {
            AddTime(TimeSpan.FromMinutes(120));
            //AddTime(TimeSpan.FromSeconds(5));
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            StopTime();
        }

        public void SendToast()
        {

            string title = "TimeBoxerApp";
            string content = "Timer is over";

            // Construct the visuals of the toast
            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText()
                        {
                            Text = title
                        },

                        new AdaptiveText()
                        {
                            Text = content
                        },

                    },
                }
            };

            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
            };

            // And create the toast notification
            var toast = new ToastNotification(toastContent.GetXml());

            toast.ExpirationTime = DateTime.Now.AddHours(1);

            toast.Tag = "1";
            toast.Group = "TimeBoxer";

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

    }


}
