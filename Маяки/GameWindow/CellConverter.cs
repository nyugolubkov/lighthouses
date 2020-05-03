using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Маяки.GameWindow
{
    /// <summary>
    /// Реализует интерфейс IValueConverter 
    /// для отображения клеток по информации из класса Cell
    /// </summary>
    public class CellConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            switch ((CellValueEnum)value)
            {
                case CellValueEnum.None:
                    return new TextBlock() { Text = ((int)value).ToString() };
                case CellValueEnum.Lighthouse:
                    return new TextBlock() { Text = ((int)value).ToString() };
                case CellValueEnum.Boat:
                    try
                    {
                        return new Image()
                        {
                            Source = new BitmapImage(new Uri(
                                ConstValues.BoatPath, UriKind.Relative))
                        };
                    }
                    catch
                    {
                        return new TextBlock() { Text = "Boat" };
                    }
                case CellValueEnum.Unavailable:
                    try
                    {
                        return new Image()
                        {
                            Source = new BitmapImage(new Uri(
                                ConstValues.UnAvailPath, UriKind.Relative))
                        };
                    }
                    catch
                    {
                        return new TextBlock() { Text = "Waves" };
                    }
                case CellValueEnum.Blocked:
                    try
                    {
                        return new Image()
                        {
                            Source = new BitmapImage(new Uri(
                                ConstValues.CrossesPath, UriKind.Relative))
                        };
                    }
                    catch
                    {
                        return new TextBlock() { Text = "Crosses" };
                    }
            }

            return (int)CellValueEnum.None;
        }

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
