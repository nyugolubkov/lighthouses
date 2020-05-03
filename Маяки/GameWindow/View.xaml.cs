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

namespace Маяки.GameWindow
{
    /// <summary>
    /// Логика взаимодействия для View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View(DeskOfCells cells)
        {
            InitializeComponent();

            DataContext = new ViewModel(cells);
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Cell cell = ((Cell)((Border)sender).DataContext);

            switch (cell.Value)
            {
                case CellValueEnum.None:
                    cell.Value = CellValueEnum.Boat;
                    break;
                case CellValueEnum.Boat:
                    cell.Value = CellValueEnum.Blocked;
                    break;
                case CellValueEnum.Blocked:
                    cell.Value = CellValueEnum.None;
                    break;
            }

            try
            {
                ((ViewModel)DataContext).Save(ConstValues.LevelsDirectoryPath);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Window.GetWindow(this).Close();
            }
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            ((GameWindow)(Window.GetWindow(this))).Timer.Stop();

            if (((ViewModel)DataContext).CheckIfRight())
            {
                uint timeNow = ((GameWindow)(Window.GetWindow(this))).TimeNow;
                string second = timeNow % 60 >= 10 ? $"{timeNow % 60}" : $"0{timeNow % 60}";
                string minute = timeNow / 60 % 60 >= 10 ?
                    $"{timeNow / 60 % 60}" : $"0{timeNow / 60 % 60}";
                string hour = $"{timeNow / 3600}";

                MessageBox.Show($"Верно! Это заняло {hour}:{minute}:{second}");
            }
            else
            {
                MessageBox.Show("Неверно");
            }

            ((GameWindow)(Window.GetWindow(this))).Timer.Start();
        }
    }
}
