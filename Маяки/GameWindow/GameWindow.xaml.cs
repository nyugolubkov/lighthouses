using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
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

        /// <summary>
        /// Конструктор, инициализирующий компоненты класса,
        /// а также запускает отсчет таймера
        /// </summary>
        /// <param name="cells">Класс - игровое поле</param>
        /// <param name="isGenerated">Сгенерированно ли поле</param>
        public GameWindow(DeskOfCells cells, bool isGenerated)
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
                    cells.TimeStart = 0;
                    cells.AmountOfTrials = (cells.Difficulty == LevelDifficultyEnum.Beginner)
                        ? 5 : ((cells.Difficulty == LevelDifficultyEnum.Improving)
                        ? 4 : 3);
                }
            }
            else
            {
                cells.TimeStart = 0;
            }

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            TimeNow = cells.TimeStart;
            timerTextBlock.Text = "0:00:00";

            viewModelControl.Content = new View(cells, isGenerated);
        }

        /// <summary>
        /// Метод обработчик события тика таймера
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeNow++;

            string second = TimeNow % 60 >= 10 ? $"{TimeNow % 60}" : $"0{TimeNow % 60}";
            string minute = TimeNow / 60 % 60 >= 10 ?
                $"{TimeNow / 60 % 60}" : $"0{TimeNow / 60 % 60}";
            string hour = $"{TimeNow / 3600}";

            timerTextBlock.Text = $"{hour}:{minute}:{second}";
        }

        /// <summary>
        /// Метод обработчик события нажатия на кнопку возврата в главное меню
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
