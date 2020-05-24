using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Маяки.MainMenuUserControls;

namespace Маяки.GameWindow
{
    /// <summary>
    /// Логика взаимодействия для View.xaml
    /// Элемент управления, содержащий игровое поле и кнопки 
    /// "Проверить" и "Очистить поле", наследуется от класса UserControl
    /// </summary>
    public partial class View : UserControl
    {
        private bool IsGenerated { get; }
        private List<List<List<string>>> records;

        /// <summary>
        /// Конструктор, инициализирующий компоненты данного элемента управления
        /// </summary>
        /// <param name="cells">Игровое поле</param>
        /// <param name="isGenerated">Сгенерированно ли поле</param>
        public View(DeskOfCells cells, bool isGenerated)
        {
            InitializeComponent();

            IsGenerated = isGenerated;

            cells.AmountOfTrials = (cells.AmountOfTrials != 0)
                ? cells.AmountOfTrials : ((cells.Difficulty == LevelDifficultyEnum.Beginner)
                ? 5 : ((cells.Difficulty == LevelDifficultyEnum.Improving)
                ? 4 : 3));

            DataContext = new ViewModel(cells);

            trialsTextBlock.Text = ConstValues.TrialsText + 
                ((ViewModel)DataContext).Mod.Cells.AmountOfTrials;
            miniRulesTextBlock.Text = ConstValues.MiniRulesText;

            records = Records.RecordDeserializer();
        }

        /// <summary>
        /// Метод обработчик события нажатия на клетку игрового поля.
        /// Меняет состояние клетки в соответствии с правилами игры.
        /// </summary>
        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Cell cell = ((Cell)((Border)sender).DataContext);

            switch (cell.Value)
            {
                case CellValueEnum.None:
                    if (((ViewModel)DataContext).Mod.Cells.Difficulty
                        == LevelDifficultyEnum.Beginner &&
                        ((ViewModel)DataContext).Mod.Cells
                        .AreAnyBoatsNear(cell.Row, cell.Column))
                    {
                        cell.Value = CellValueEnum.Blocked;
                        break;
                    }
                    cell.Value = CellValueEnum.Boat;
                    break;
                case CellValueEnum.Boat:
                    cell.Value = CellValueEnum.Blocked;
                    break;
                case CellValueEnum.Blocked:
                    cell.Value = CellValueEnum.None;
                    break;
                case CellValueEnum.Lighthouse:
                    ((ViewModel)DataContext).Mod.Cells
                        .MakeCrosses(cell.Row, cell.Column);
                    break;
            }

            if (!IsGenerated)
            {
                try
                {
                    ((ViewModel)DataContext).Save(ConstValues.LevelsDirectoryPath,
                        ((GameWindow)(Window.GetWindow(this))).TimeNow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Window.GetWindow(this).Close();
                }
            }
        }

        /// <summary>
        /// Метод обработчик события нажатия на кнопку проверки.
        /// Проверяет, правильной ли была попытка решить головоломку.
        /// </summary>
        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            ((GameWindow)(Window.GetWindow(this))).Timer.Stop();

            if (((ViewModel)DataContext).CheckIfRight())
            {
                OnRightTrialReact();
            }
            else
            {
                OnWrongTrialReact();
            }

            ((GameWindow)(Window.GetWindow(this))).Timer.Start();

            if (!IsGenerated)
            {
                try
                {
                    ((ViewModel)DataContext).Save(ConstValues.LevelsDirectoryPath,
                        ((GameWindow)(Window.GetWindow(this))).TimeNow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Window.GetWindow(this).Close();
                }
            }
        }

        /// <summary>
        /// Метод, вызываемый при правильном решении головоломки
        /// </summary>
        private void OnRightTrialReact()
        {
            uint timeNow = ((GameWindow)(Window.GetWindow(this))).TimeNow;
            string second = timeNow % 60 >= 10 ? $"{timeNow % 60}" : $"0{timeNow % 60}";
            string minute = timeNow / 60 % 60 >= 10 ?
                $"{timeNow / 60 % 60}" : $"0{timeNow / 60 % 60}";
            string hour = $"{timeNow / 3600}";

            CheckIFTimeNowIsRecord(timeNow);
           
            try
            {
                SoundPlayer sp = new SoundPlayer();
                sp.SoundLocation = ConstValues.WinSoundPath;
                sp.Load();
                sp.Play();
            }
            catch (Exception) { }

            MessageBox.Show($"Верно! Поздравляю! Это заняло {hour}:{minute}:{second}");

            new MainWindow().Show();
            ((GameWindow)(Window.GetWindow(this))).Close();
        }

        /// <summary>
        /// Метод, вызываемый при неправильном решении головоломки.
        /// </summary>
        private void OnWrongTrialReact()
        {
            ((ViewModel)DataContext).Mod.Cells.AmountOfTrials--;
            trialsTextBlock.Text = ConstValues.TrialsText + 
                ((ViewModel)DataContext).Mod.Cells.AmountOfTrials;

            if (((ViewModel)DataContext).Mod.Cells.AmountOfTrials == 0)
            {
                try
                {
                    SoundPlayer sp = new SoundPlayer();
                    sp.SoundLocation = ConstValues.GameOverSoundPath;
                    sp.Load();
                    sp.Play();
                }
                catch (Exception) { }

                MessageBox.Show($"Неверно. У вас больше не осталось попыток.");

                new MainWindow().Show();
                ((GameWindow)(Window.GetWindow(this))).Close();
                return;
            }

            if (((ViewModel)DataContext).Mod.Cells.Difficulty
                == LevelDifficultyEnum.Experienced)
            {
                MessageBox.Show($"Неверно. Осталось попыток: " +
                    $"{((ViewModel)DataContext).Mod.Cells.AmountOfTrials}.");
                return;
            }

            var ug = (Border)VisualTreeHelper.GetChild(itemsControl, 0);
            var ug2 = VisualTreeHelper.GetChild(ug, 0);
            var ug3 = VisualTreeHelper.GetChild(ug2, 0);

            List<Cell> wrongCells = ((ViewModel)DataContext).Mod.Cells.WrongLocatedBoats();

            if (wrongCells.Count == 0)
            {
                MessageBox.Show($"Неверно. Недостачное количество кораблей. Осталось попыток: " +
                    $"{((ViewModel)DataContext).Mod.Cells.AmountOfTrials}.");
                return;
            }

            ChangeWrongColors(wrongCells, (UniformGrid)ug3, Colors.Red);

            MessageBox.Show($"Неверно. Некоторые кораблики были поставлены на ошибочное место. " +
                $"Осталось попыток: {((ViewModel)DataContext).Mod.Cells.AmountOfTrials}.");

            ChangeWrongColors(wrongCells, (UniformGrid)ug3, Colors.White);
        }

        /// <summary>
        /// Метод, меняющий цвет фона некоторых клеток
        /// </summary>
        /// <param name="wrongCells">Клетки, цвет фона которых будет изменен</param>
        /// <param name="ug">UniformGrid, где находятся клетки</param>
        /// <param name="color">Новый цвет данных клеток</param>
        private void ChangeWrongColors(List<Cell> wrongCells, 
            UniformGrid ug, Color color)
        {
            foreach (var item in wrongCells)
            {
                var border = VisualTreeHelper.GetChild(ug, (item.Row - 1) * 
                    ((ViewModel)DataContext).Mod.Cells.FieldSize + (item.Column - 1));
                ((Border)VisualTreeHelper.GetChild(border, 0))
                    .Background = new SolidColorBrush(color);
            }
        }

        /// <summary>
        /// Преобразует время, сохраненное в качестве string, в количество секунд
        /// </summary>
        /// <param name="time">Время в строке</param>
        /// <returns>Количество секунд</returns>
        public uint StringToTime(string time)
        {
            uint uintTime;

            try
            {
                string[] ticks = time.Split(new char[] { ':' },
                  StringSplitOptions.RemoveEmptyEntries);
                uintTime = uint.Parse(ticks[2]) +
                    uint.Parse(ticks[1]) * 60 + uint.Parse(ticks[0]) * 3600;
            }
            catch (Exception)
            {
                uintTime = uint.MaxValue;
            }

            return uintTime;
        }

        /// <summary>
        /// Проверяет, является ли время рекордом, и выполняет действия,
        /// соответствующие результату проверки
        /// </summary>
        /// <param name="timeNow">Время решения головоломки</param>
        private void CheckIFTimeNowIsRecord(uint timeNow)
        {
            int[] dimensions = new int[2];

            switch (((ViewModel)DataContext).Mod.Cells.FieldSize)
            {
                case 6:
                    dimensions[0] = 0;
                    break;
                case 8:
                    dimensions[0] = 1;
                    break;
                case 10:
                    dimensions[0] = 2;
                    break;
                case 12:
                    dimensions[0] = 3;
                    break;
            }

            switch (((ViewModel)DataContext).Mod.Cells.Difficulty)
            {
                case LevelDifficultyEnum.Beginner:
                    dimensions[1] = 0;
                    break;
                case LevelDifficultyEnum.Improving:
                    dimensions[1] = 1;
                    break;
                case LevelDifficultyEnum.Experienced:
                    dimensions[1] = 2;
                    break;
            }

            var rec = Array.ConvertAll(records[dimensions[0]][dimensions[1]].ToArray(),
                x => StringToTime(x));

            Array.Sort(rec);

            if (timeNow >= rec[2])
                return;

            if (timeNow < rec[0])
            {
                rec[2] = rec[1];
                rec[1] = rec[0];
                rec[0] = timeNow;
            }
            else if (timeNow < rec[1])
            {
                rec[2] = rec[1];
                rec[1] = timeNow;
            }
            else
            {
                rec[2] = timeNow;
            }

            records[dimensions[0]][dimensions[1]].Clear();

            foreach (var item in rec)
            {
                if (item == uint.MaxValue)
                {
                    records[dimensions[0]][dimensions[1]].Add("-");
                    continue;
                }

                string second = item % 60 >= 10 ? $"{item % 60}" : $"0{item % 60}";
                string minute = item / 60 % 60 >= 10 ?
                    $"{item / 60 % 60}" : $"0{item / 60 % 60}";
                string hour = $"{item / 3600}";

                records[dimensions[0]][dimensions[1]].Add($"{hour}:{minute}:{second}");
            }

            Records.SaveRecords(records);
        }
    }
}
