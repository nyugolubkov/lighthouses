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
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            control.Content = new MainMenu(control);
        }

        private void Label_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            GameWindow.GameWindow gameWindow = new
                GameWindow.GameWindow((DeskOfCells)listBox.SelectedItem);
            gameWindow.Show();
            Window.GetWindow(this).Close();
        }
    }
}
