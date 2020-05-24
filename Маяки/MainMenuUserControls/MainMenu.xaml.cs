using System.Windows.Controls;

namespace Маяки.MainMenuUserControls
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// Элемент управления, показывающий главное меню программы, 
    /// наследник класса UserControl
    /// </summary>
    public partial class MainMenu : UserControl
    {
        /// <summary>
        /// Конструктор, инициализирующий компоненты этого элемента управления
        /// </summary>
        public MainMenu(object sender)
        {
            InitializeComponent();
            gameMenu.Content = new PlayRules(sender, gameMenu);
        }
    }
}