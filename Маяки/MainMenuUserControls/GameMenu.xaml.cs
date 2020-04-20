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

namespace Маяки.MainMenuUserControls
{
    /// <summary>
    /// Логика взаимодействия для GameMenu.xaml
    /// </summary>
    public partial class GameMenu : UserControl
    {
        private readonly ContentControl mainControl;

        public GameMenu(object sender1)
        {
            InitializeComponent();
            mainControl = (ContentControl)sender1;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new MainMenu(mainControl);
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow.GameWindow gameWindow = new GameWindow.GameWindow();
            gameWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void LoadGameButton_Click(object sender, RoutedEventArgs e)
        {
            mainControl.Content = new Loads(mainControl);
        }
    }
}
