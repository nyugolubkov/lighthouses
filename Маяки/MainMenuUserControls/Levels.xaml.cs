using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Маяки.GameWindow;

namespace Маяки.MainMenuUserControls
{
    /// <summary>
    /// Логика взаимодействия для Levels.xaml
    /// Элемент управления, на котором распложено меню выбора уровня,
    /// наследник класса UserControl
    /// </summary>
    public partial class Levels : UserControl
    {
        private readonly ContentControl control;
        List<DeskOfCells> blockOfCells;

        /// <summary>
        /// Конструктор, инициализирующий компоненты этого элемента управления
        /// </summary>
        public Levels(object sender)
        {
            InitializeComponent();
            control = (ContentControl)sender;

            try
            {
                blockOfCells = Cell.Deserialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Window.GetWindow(this).Close();
            }

            listBox.ItemsSource = blockOfCells;
        }

        /// <summary>
        /// Метод обработчик события нажатия на кнопку возврата в главное меню
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            control.Content = new MainMenu(control);
        }

        /// <summary>
        /// Метод обработчик события двойного клика на Label с уровнем
        /// </summary>
        private void Label_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            GameWindow.GameWindow gameWindow = new
                GameWindow.GameWindow((DeskOfCells)listBox.SelectedItem, false);
            gameWindow.Show();
            Window.GetWindow(this).Close();
        }
    }
}
