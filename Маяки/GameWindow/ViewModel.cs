using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;

namespace Маяки.GameWindow
{
    /// <summary>
    /// Класс, содержащий все нужные для основного процесса игры данные и методы,
    /// наследуется от класса OnPropertyChangedClass
    /// </summary>
    public class ViewModel : OnPropertyChangedClass
    {
        Model model;
        private IEnumerable<Cell> _cells;
        public RelayCommand RestartCommand { get; }
        public RelayCommand RulesCommand { get; }
        public Model Mod => model;

        /// <summary>
        /// Конструктор, инициализирующий компоненты данного класса
        /// </summary>
        /// <param name="allCells">Игровое поле</param>
        public ViewModel(DeskOfCells allCells)
        {
            model = new Model(allCells);
            model.PropertyChanged += Model_PropertyChanged;

            List<Cell> cells = new List<Cell>();

            foreach (Cell cell in model.Cells)
            {
                cells.Add(cell);
            }

            Cells = cells;

            RestartCommand = new RelayCommand(par => model.Cells.Clear());
            RulesCommand = new RelayCommand(par => MessageBox.Show(ConstValues.Rules, "Правила игры"));
        }

        /// <summary>
        /// Метод обработчик события смены состояния модели
        /// </summary>
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string propertyName = e.PropertyName;
            if (string.IsNullOrEmpty(propertyName))
                OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Метод, сохраняющий прогресс выполняемого поля
        /// </summary>
        /// <param name="path">Путь файла</param>
        /// <param name="timeStart">Нынешнее время</param>
        public void Save(string path, uint timeStart)
        {
            model.Cells.TimeStart = timeStart;

            DataContractJsonSerializer ser = new
                DataContractJsonSerializer(typeof(DeskOfCells));

            using (FileStream fs = new FileStream(path + '\\' + model.Cells.Name + ".json", 
                FileMode.Create))
            {
                ser.WriteObject(fs, model.Cells);
            }
        }

        public IEnumerable<Cell> Cells
        {
            get => _cells;
            private set
            {
                _cells = value;
                OnPropertyChanged("");
            }
        }

        /// <summary>
        /// Метод, проверяющий, правильно ли решена головоломка
        /// </summary>
        /// <returns>Результат проверки</returns>
        public bool CheckIfRight() => model.Cells.Check();
    }
}
