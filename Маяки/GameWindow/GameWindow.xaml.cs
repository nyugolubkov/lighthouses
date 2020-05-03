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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Маяки.GameWindow
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// Окно, являющееся игровым полем, наследник класса Window
    /// </summary>
    public partial class GameWindow : Window
    {
        private readonly DispatcherTimer timer;
        public DispatcherTimer Timer => timer;

        public uint TimeNow { get; private set; }

        public GameWindow(DeskOfCells cells)
        {
            InitializeComponent();

            if (File.Exists(ConstValues.LightHousePath))
            {
                try
                {
                    Icon = new BitmapImage(new Uri(ConstValues.LightHousePath, UriKind.Relative));
                }
                catch (Exception) { }
            }


            if (!cells.IsClear())
            {
                MessageBoxResult res = MessageBox.Show(ConstValues.ContinueLastGame,
                    ConstValues.ContinueTitle, MessageBoxButton.YesNo);

                if (res == MessageBoxResult.No)
                {
                    cells.Clear();
                }
            }

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            TimeNow = 0;
            timerTextBlock.Text = "0:00:00";

            viewModelControl.Content = new View(cells);
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
