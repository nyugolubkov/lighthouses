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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Маяки.GameWindow
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private readonly DispatcherTimer timer;

        private uint TimeNow { get; set; }

        public GameWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            TimeNow = 0;
            timerTextBlock.Text = "0:00:00";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeNow++;

            string second = TimeNow % 60 >= 10 ? $"{TimeNow % 60}" : $"0{TimeNow % 60}";
            string minute = TimeNow / 60 % 60 >= 10 ?
                $"{TimeNow / 60 % 60}" : $"0{TimeNow / 60 % 60}";
            string hour = $"{TimeNow / 3600}";

            timerTextBlock.Text = $"{hour}:{minute}:{second}";
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
