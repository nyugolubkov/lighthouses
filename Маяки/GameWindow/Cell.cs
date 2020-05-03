using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Маяки.GameWindow
{
    /// <summary>
    /// Класс, описывающий клетку игрового поля и реализующий методы, 
    /// связанные с поведением клетки, является наследником класса OnPropertyChangedClass
    /// </summary>
    [DataContract]
    public class Cell : OnPropertyChangedClass
    {
        [DataMember]
        CellValueEnum _value;
        [DataMember]
        private uint amount;

        public Cell(int row, int column, CellValueEnum value = CellValueEnum.None)
        {
            _value = value;
            Row = row;
            Column = column;
        }

        public CellValueEnum Value
        {
            get => _value;
            set
            {
                _value = value;

                OnPropertyChanged();
            }
        }

        [DataMember]
        public int Row { get; private set; }
        [DataMember]
        public int Column { get; private set; }
        public bool IsLightHouse => Value == CellValueEnum.Lighthouse;
        public uint AmountOfLightedBoats
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }

        public static List<DeskOfCells> Deserialize()
        {
            List<DeskOfCells> blockOfCells = new List<DeskOfCells>();

            string[] filesPath = Directory.GetFiles(ConstValues.LevelsDirectoryPath);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<List<Cell>>));

            foreach (string path in filesPath)
            {
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        List<List<Cell>> cells = (List<List<Cell>>)ser.ReadObject(fs);

                        blockOfCells.Add(Cell.ListToArray(cells, path));
                    }
                }
                catch (Exception) { }
            }

            return blockOfCells;
        }

        public static DeskOfCells ListToArray(List<List<Cell>> cells, string path)
        {
            if (cells.Count != 10)
                throw new ArgumentException("Wrong amount of rows!");

            string[] arrCellsName = path.Split(new char[] { '\\', '/' },
                StringSplitOptions.RemoveEmptyEntries);
            string name = arrCellsName[arrCellsName.Length - 1].Split('.')[0];

            DeskOfCells arrCells = new DeskOfCells(name);

            for (int i = 0; i < 10; i++)
            {
                if (cells[i].Count != 10)
                    throw new ArgumentException("Wrong amount of columns!");

                for (int j = 0; j < 10; j++)
                {
                    arrCells[i, j] = cells[i][j];
                }
            }

            return arrCells;
        }
    }
}
