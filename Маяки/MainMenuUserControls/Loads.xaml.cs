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
    /// Логика взаимодействия для Loads.xaml
    /// </summary>
    public partial class Loads : UserControl
    {
        private readonly ContentControl control;

        public Loads(object sender)
        {
            InitializeComponent();
            control = (ContentControl)sender;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            control.Content = new MainMenu(control);
        }
    }
}
