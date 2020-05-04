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
    /// Логика взаимодействия для MainMenu.xaml
    /// Элемент управления, показывающий главное меню программы, 
    /// наследник класса UserControl
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu(object sender)
        {
            InitializeComponent();
            gameMenu.Content = new PlayRules(sender);
        }
    }
}