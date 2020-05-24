using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

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
        private CellValueEnum _value;
        [DataMember]
        private uint amount;

        /// <summary>
        /// Конструктор, инициализирующий значение, строку и столбец клетки
        /// </summary>
        /// <param name="row">Номер строки</param>
        /// <param name="column">Номер столбца</param>
        /// <param name="value">Значение клетки</param>
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
        public uint AmountOfLightedBoats
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Десериализует все базовые поля из соответствующего хранилища.
        /// Возвращает list найденных полей.
        /// </summary>
        /// <returns>list найденных полей</returns>
        public static List<DeskOfCells> Deserialize()
        {
            List<DeskOfCells> blockOfCells = new List<DeskOfCells>();

            string[] filesPath = Directory.GetFiles(ConstValues.LevelsDirectoryPath);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DeskOfCells));

            foreach (string path in filesPath)
            {
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        DeskOfCells cells = (DeskOfCells)ser.ReadObject(fs);

                        if (!cells.IsCorrect())
                            throw new ArgumentException();

                        blockOfCells.Add(cells);
                    }
                }
                catch (Exception) { }
            }

            return blockOfCells;
        }
    }
}
