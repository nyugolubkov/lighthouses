using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Маяки.MainMenuUserControls;

namespace Маяки
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// Окно с главным меню приложения, наследник класса Window
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор, инициализирующий компоненты класса
        /// </summary>
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
