using System.Windows;
using System.Windows.Controls;

namespace Маяки.MainMenuUserControls
{
    /// <summary>
    /// Логика взаимодействия для GameMenu.xaml
    /// Элемент управления, на котором расположено меню выбора режима игры,
    /// наследник класса UserControl
    /// </summary>
    public partial class GameMenu : UserControl
    {
        private readonly ContentControl mainControl;

        /// <summary>
        /// Конструктор, инициализирующий компоненты этого элемента управления
        /// </summary>
        public GameMenu(object sender)
        {
            InitializeComponent();

            mainControl = (ContentControl)sender;
        }

        /// <summary>
        /// Метод обработчик события нажатия на кнопку начала игры
        /// </summary>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new Levels(mainControl);
        }

        /// <summary>
        /// Метод обработчик события нажатия на кнопку генерирования игрового поля
        /// </summary>
        private void GenerateLevelButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new ChooseDifficulty(mainControl);
        }

        /// <summary>
        /// Метод обработчик события нажатия на кнопку возврата в главное меню
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new MainMenu(mainControl);
        }
    }
}
