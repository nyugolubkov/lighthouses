using System.Windows;
using System.Windows.Controls;
using Маяки.GameWindow;

namespace Маяки.MainMenuUserControls
{
    /// <summary>
    /// Логика взаимодействия для ChooseField.xaml
    /// Элемент управления, на котором расположено меню выбора размера игрового поля.
    /// Является наследником UserControl.
    /// </summary>
    public partial class ChooseField : UserControl
    {
        private readonly ContentControl control;
        private readonly LevelDifficultyEnum difficulty;

        /// <summary>
        /// Конструктор, инициализирующий компоненты этого элемента управления
        /// </summary>
        /// <param name="sender">ContentControl на котором расположен данный UserControl</param>
        /// <param name="diff">Сложность уровня</param>
        public ChooseField(object sender, LevelDifficultyEnum diff)
        {
            InitializeComponent();
            control = (ContentControl)sender;
            difficulty = diff;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку возвращения в главное меню
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            control.Content = new MainMenu(control);
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку с размером поля
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int fieldSize;
            if (!int.TryParse(((Button)sender).Content.ToString().Split('x')[0],
                    out fieldSize))
            {
                fieldSize = 10;
            }

            DeskOfCells cells = FieldGenerator.Generator(fieldSize);
            cells.Difficulty = difficulty;

            GameWindow.GameWindow gameWindow = new
                    GameWindow.GameWindow(cells, true);
            gameWindow.Show();
            Window.GetWindow(this).Close();
        }
    }
}
