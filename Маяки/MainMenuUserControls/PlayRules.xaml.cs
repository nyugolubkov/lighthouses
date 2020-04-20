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
    /// Логика взаимодействия для PlayRules.xaml
    /// </summary>
    public partial class PlayRules : UserControl
    {
        private readonly ContentControl mainControl;
        private readonly ContentControl control;

        public PlayRules(object sender1, object sender2)
        {
            InitializeComponent();

            mainControl = (ContentControl)sender1;
            control = (ContentControl)sender2;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            control.Content = new GameMenu(mainControl);
        }

        private void RulesButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Правила игры...");
        }
    }
}
