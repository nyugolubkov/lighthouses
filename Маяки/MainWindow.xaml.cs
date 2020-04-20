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
        private const string backGroundPath = @"..\..\Resources\Images\Waves.jpg";
        private const string lightHousePath = @"..\..\Resources\Images\Lighthouse.png";

        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists(backGroundPath))
            {
                try
                {
                    Background = new ImageBrush(new BitmapImage(
                        new Uri(backGroundPath, UriKind.Relative)));
                }
                catch(Exception) { }
            }

            if (File.Exists(lightHousePath))
            {
                try
                {
                    Icon = new BitmapImage(new Uri(lightHousePath, UriKind.Relative));
                    lightHouse1.Source = Icon;
                    lightHouse2.Source = Icon;
                }
                catch(Exception) { }
            }

            mainMenuControl.Content = new MainMenu(mainMenuControl);
        }
    }
}
