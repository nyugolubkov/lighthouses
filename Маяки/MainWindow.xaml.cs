using System;
using System.Collections.Generic;
using System.IO;
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
using Маяки.MainMenuUserControls;

namespace Маяки
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists(ConstValues.BackGroundPath))
            {
                try
                {
                    Background = new ImageBrush(new BitmapImage(
                        new Uri(ConstValues.BackGroundPath, UriKind.Relative)));
                }
                catch(Exception) { }
            }

            if (File.Exists(ConstValues.LightHousePath))
            {
                try
                {
                    Icon = new BitmapImage(new Uri(ConstValues.LightHousePath, UriKind.Relative));
                    lightHouse1.Source = Icon;
                    lightHouse2.Source = Icon;
                }
                catch(Exception) { }
            }

            mainMenuControl.Content = new MainMenu(mainMenuControl);
        }
    }
}
