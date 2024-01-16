using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Time_Counter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int COUNTDOWN = 0;
        //private int timeHour = 0;
        //private int timeMinute = 1;
        //private int timeSecond = 0;
        private int time = 0;
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            BStart.Click += BStart_Click;

            ListBoxItem LBcountDown = new ListBoxItem();
            LBcountDown.Content = "CountDown";
            LBcountDown.Tag = COUNTDOWN;
            CBActions.Items.Add(LBcountDown);

            CBActions.SelectedIndex = 0;
        }

        private void BStart_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem? selectedItem = CBActions.SelectedItem as ListBoxItem; 
            if(selectedItem == null) return;
            
            //TODO: reset function here
            if(timer != null) timer.Stop();
            TBCountDown.Foreground = Brushes.Black;

            time = 0;
            switch (selectedItem.Tag)
            {
                case COUNTDOWN:
                    try
                    {
                        int num = Int32.Parse(TBTime.Text);
                        time = num;
                    }
                    catch (FormatException exception)
                    {
                        MessageBox.Show(exception.Message);
                        TBTime.Text = "0";
                        return;
                    }
                    TBCountDown.Text = string.Format("{0}:{1}:{2}", time / 360, time / 60, time % 60);
                    timer = new DispatcherTimer();
                    timer.Interval = new TimeSpan(0, 0, 1);
                    timer.Tick += TimerCountDownTick;
                    timer.Start();
                    break;

                default:
                    break;
            }
           
            
        }
        void TimerCountDownTick(object? sender, EventArgs e)
        {
            if(time > 0)
            {
                if(time <= 10)
                {
                    TBCountDown.Foreground = Brushes.Red;
                }

                time--;
                TBCountDown.Text = string.Format("{0}:{1}:{2}", time / 360, time / 60, time % 60);
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Stop!");
            }
        }
    }
}