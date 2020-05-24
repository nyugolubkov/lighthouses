using System.Windows;
using System.Windows.Controls;

namespace Маяки.MainMenuUserControls
{
    /// <summary>
    /// Логика взаимодействия для Info.xaml
    /// Элемент управления, на котором показана информация 
    /// о создателе программы, наследник класса UserControl
    /// </summary>
    public partial class Info : UserControl
    {
        private readonly ContentControl control;

        /// <summary>
        /// Конструктор, инициализирующий компоненты этого элемента управления
        /// </summary>
        public Info(object sender)
        {
            InitializeComponent();

            control = (ContentControl)sender;

            infoTextBlock.Text = ConstValues.InfoText;
        }

        /// <summary>
        /// Метод обработчик события нажатия на кнопку возврата в главное меню
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            control.Content = new MainMenu(control);
        }
    }
}
