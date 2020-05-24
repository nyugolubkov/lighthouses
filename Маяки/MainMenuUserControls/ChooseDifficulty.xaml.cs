using System.Windows;
using System.Windows.Controls;
using Маяки.GameWindow;

namespace Маяки.MainMenuUserControls
{
    /// <summary>
    /// Логика взаимодействия для ChooseDifficulty.xaml
    /// Элемент управления, на котором расположено меню выбора сложности игры.
    /// Является наследником UserControl.
    /// </summary>
    public partial class ChooseDifficulty : UserControl
    {
        private readonly ContentControl control;

        /// <summary>
        /// Конструктор, инициализирующий компоненты этого элемента управления
        /// </summary>
        /// <param name="sender">ContentControl на котором расположен данный UserControl</param>
        public ChooseDifficulty(object sender)
        {
            InitializeComponent();
            control = (ContentControl)sender;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку со сложностью поля
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LevelDifficultyEnum diff;

            switch (((Button)sender).Content.ToString())
            {
                case "Новичок":
                    diff = LevelDifficultyEnum.Beginner;
                    break;
                case "Совершенствующийся":
                    diff = LevelDifficultyEnum.Improving;
                    break;
                default:
                    diff = LevelDifficultyEnum.Experienced;
                    break;
            }

            control.Content = new ChooseField(control, diff);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку возвращения в главное меню
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            control.Content = new MainMenu(control);
        }
    }
}
