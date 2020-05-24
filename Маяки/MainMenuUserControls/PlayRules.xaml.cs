using System.Windows;
using System.Windows.Controls;

namespace Маяки.MainMenuUserControls
{
    /// <summary>
    /// Логика взаимодействия для PlayRules.xaml
    /// Элемент управления, на котором расположены кнопки главного меню приложения
    /// </summary>
    public partial class PlayRules : UserControl
    {
        private readonly ContentControl mainControl;
        private readonly ContentControl control;

        /// <summary>
        /// Конструктор, инициализирующий компоненты этого элемента управления
        /// </summary>
        public PlayRules(object sender1, object sender2)
        {
            InitializeComponent();

            mainControl = (ContentControl)sender1;
            control = (ContentControl)sender2;
        }

        /// <summary>
        /// Метод обработчик события нажатия на кнопку начала игры
        /// </summary>
        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            control.Content = new GameMenu(mainControl);
        }

        /// <summary>
        /// Метод обработчик события нажатия на кнопку показа информации о программе
        /// </summary>
        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new Info(mainControl);
        }

        /// <summary>
        /// Метод обработчик события нажатия на кнопку показа рекордов игры
        /// </summary>
        private void RecordsButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new Records(mainControl);
        }
    }
}
