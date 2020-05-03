using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Маяки.GameWindow
{
    public class ViewModel : OnPropertyChangedClass
    {
        Model model;
        private IEnumerable<Cell> _cells;
        public RelayCommand RestartCommand { get; }

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
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string propertyName = e.PropertyName;
            if (string.IsNullOrEmpty(propertyName))
                OnPropertyChanged(propertyName);
        }


        public void Save(string path)
        {
            DataContractJsonSerializer ser = new 
                DataContractJsonSerializer(typeof(List<IEnumerable<Cell>>));

            List <IEnumerable<Cell>> serList = new List<IEnumerable<Cell>>();

            for (int i = 0; i < 10; i++)
            {
                serList.Add(Cells.Skip(10 * i).Take(10));
            }

            using (FileStream fs = new FileStream(path + '\\' + model.Cells.Name + ".json", 
                FileMode.Create))
            {
                ser.WriteObject(fs, serList);
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

        public bool CheckIfRight() => model.Cells.Check();
    }
}
