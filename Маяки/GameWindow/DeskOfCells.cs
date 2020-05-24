using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Маяки.GameWindow
{
    /// <summary>
    /// Класс - коллекция, отвечающий за хранение информации об игровом поле и
    /// реализующий методы работы с полем. Также, реализует интерфейс IEnumerable
    /// </summary>
    [DataContract]
    public class DeskOfCells : IEnumerable<Cell>
    {
        [DataMember]
        private List<List<Cell>> cells;
        [DataMember]
        public uint TimeStart { get; set; }
        [DataMember]
        public LevelDifficultyEnum Difficulty { get; set; }
        [DataMember]
        public int AmountOfTrials { get; set; }
        public string Info => Name + $" {FieldSize}x{FieldSize} {ConvertDifficulty}";
        private string ConvertDifficulty => Difficulty == LevelDifficultyEnum.Beginner ? "Новичок"
            : (Difficulty == LevelDifficultyEnum.Improving ? "Совершенствующийся" : "Опытный игрок");
        public int FieldSize => cells.Count - 2;

        /// <summary>
        /// Конструктор, инициализирующий элементы класса игрового поля
        /// и создающий элементы клетки с пустыми значениями
        /// </summary>
        /// <param name="name">Название поля</param>
        /// <param name="fieldSize">Размер поля</param>
        public DeskOfCells(string name, int fieldSize)
        {
            Name = name;
            cells = new List<List<Cell>>(fieldSize + 2);

            for (int i = 0; i < fieldSize + 2; i++)
            {
                List<Cell> newCells = new List<Cell>(fieldSize + 2);

                for (int j = 0; j < fieldSize + 2; j++)
                {
                    newCells.Add(new Cell(i, j));
                }

                cells.Add(newCells);
            }

            TimeStart = 0;
            AmountOfTrials = 0;
        }

        [DataMember]
        public string Name { get; private set; }

        public Cell this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i >= FieldSize || j >= FieldSize)
                    throw new FormatException("Out of array borders!");

                return cells[i + 1][j + 1];
            }
            set
            {
                if (i < 0 || j < 0 || i >= FieldSize || j >= FieldSize)
                    throw new FormatException("Out of array borders!");

                cells[i + 1][j + 1] = value;
            }
        }

        /// <summary>
        /// Считает количество кораблей, находящихся на одной строке
        /// или на одном столбце с клеткой с данными координатами (если это маяк).
        /// </summary>
        /// <param name="row">Строка клетки</param>
        /// <param name="column">Столбец клетки</param>
        /// <returns>Количество корабликов</returns>
        public uint CountBoats(int row, int column)
        {
            if (this[row, column].Value != CellValueEnum.Lighthouse)
                throw new ArgumentException("Wrong cell!");

            uint count = 0;

            for (int i = 1; i <= FieldSize; i++)
            {
                if (cells[i][column + 1].Value == CellValueEnum.Boat)
                    count++;

                if (cells[row + 1][i].Value == CellValueEnum.Boat)
                    count++;
            }

            return count;
        }

        /// <summary>
        /// Проверяет, правильно ли решена головоломка
        /// </summary>
        /// <returns>Результат проверки</returns>
        public bool Check()
        {
            int count = 0;

            for (int i = 1; i <= FieldSize; i++)
            {
                for (int j = 1; j <= FieldSize; j++)
                {
                    if (cells[i][j].Value == CellValueEnum.Boat)
                        count++;

                    if (count > FieldSize)
                        return false;

                    if (cells[i][j].Value == CellValueEnum.Boat &&
                        AreAnyBoatsNear(i, j))
                        return false;

                    if (cells[i][j].Value == CellValueEnum.Lighthouse &&
                        !IsLightHouseComplete(i, j))
                        return false;
                }
            }

            return count == FieldSize;
        }

        /// <summary>
        /// Проверяет, есть ли рядом с данной клеткой кораблики или маяки
        /// </summary>
        /// <param name="row">Строка клетки</param>
        /// <param name="column">Столбец клетки</param>
        /// <returns>Результат проверки</returns>
        public bool AreAnyBoatsNear(int row, int column)
            => cells[row + 1][column].Value == CellValueEnum.Boat ||
                cells[row][column + 1].Value == CellValueEnum.Boat ||
                cells[row + 1][column + 1].Value == CellValueEnum.Boat ||
                cells[row - 1][column].Value == CellValueEnum.Boat ||
                cells[row][column - 1].Value == CellValueEnum.Boat ||
                cells[row - 1][column - 1].Value == CellValueEnum.Boat ||
                cells[row + 1][column - 1].Value == CellValueEnum.Boat ||
                cells[row - 1][column + 1].Value == CellValueEnum.Boat || 
                cells[row + 1][column].Value == CellValueEnum.Lighthouse ||
                cells[row][column + 1].Value == CellValueEnum.Lighthouse ||
                cells[row + 1][column + 1].Value == CellValueEnum.Lighthouse ||
                cells[row - 1][column].Value == CellValueEnum.Lighthouse ||
                cells[row][column - 1].Value == CellValueEnum.Lighthouse ||
                cells[row - 1][column - 1].Value == CellValueEnum.Lighthouse ||
                cells[row + 1][column - 1].Value == CellValueEnum.Lighthouse ||
                cells[row - 1][column + 1].Value == CellValueEnum.Lighthouse;

        /// <summary>
        /// Меняет состояние клеток вокруг данной на крестики
        /// </summary>
        /// <param name="row">Строка клетки</param>
        /// <param name="column">Столбец клетки</param>
        public void MakeCrosses(int row, int column)
        {
            cells[row + 1][column].Value = CellValueEnum.Blocked;
            cells[row][column + 1].Value = CellValueEnum.Blocked;
            cells[row + 1][column + 1].Value = CellValueEnum.Blocked;
            cells[row - 1][column].Value = CellValueEnum.Blocked;
            cells[row][column - 1].Value = CellValueEnum.Blocked;
            cells[row - 1][column - 1].Value = CellValueEnum.Blocked;
            cells[row + 1][column - 1].Value = CellValueEnum.Blocked;
            cells[row - 1][column + 1].Value = CellValueEnum.Blocked;
        }

        /// <summary>
        /// Проверяет, совпадает ли количество корабликов на одной строке или 
        /// на одном столбце с числом на маяке
        /// </summary>
        /// <param name="row">Строка клетки</param>
        /// <param name="column">Столбец клетки</param>
        /// <returns>Результат проверки</returns>
        public bool IsLightHouseComplete(int row, int column)
        {
            int count = 0;

            for (int i = 1; i <= FieldSize; i++)
            {
                if (cells[i][column].Value == CellValueEnum.Boat)
                    count++;

                if (cells[row][i].Value == CellValueEnum.Boat)
                    count++;
            }

            return count == cells[row][column].AmountOfLightedBoats;
        }

        /// <summary>
        /// Проверяет, есть ли на поле кораблики или крестики
        /// </summary>
        /// <returns>Результат проверки</returns>
        public bool IsClear()
        {
            foreach (var item in this)
            {
                if (item.Value == CellValueEnum.Boat ||
                    item.Value == CellValueEnum.Blocked)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Очищает поле от корабликов и крестиков
        /// </summary>
        public void Clear()
        {
            foreach (var cell in this)
            {
                cell.Value = (cell.Value == CellValueEnum.Boat ||
                    cell.Value == CellValueEnum.Blocked)
                    ? CellValueEnum.None
                    : cell.Value;
            }
        }

        /// <summary>
        /// Проверяет, правильно ли сформированна коллекция cells
        /// </summary>
        /// <returns>Результат проверки</returns>
        public bool IsCorrect()
        {
            foreach(var elem in cells)
            {
                if (elem.Count != cells.Count)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Возвращает коллекцию клеток с корабликами, 
        /// которые расположены неправильно (не по правилам)
        /// </summary>
        /// <returns>Коллекция таких клеток</returns>
        public List<Cell> WrongLocatedBoats()
        {
            List<Cell> wrongCells = new List<Cell>();

            for (int i = 1; i <= FieldSize; i++)
            {
                for (int j = 1; j <= FieldSize; j++)
                {
                    if (cells[i][j].Value == CellValueEnum.Boat && 
                        AreAnyBoatsNear(i, j))
                    {
                        wrongCells.Add(cells[i][j]);
                    }

                    if (cells[i][j].Value == CellValueEnum.Lighthouse &&
                        !IsLightHouseComplete(i, j))
                    {
                        for (int k = 1; k <= FieldSize; k++)
                        {
                            if (cells[i][k].Value == CellValueEnum.Boat)
                                wrongCells.Add(cells[i][k]);

                            if (cells[k][j].Value == CellValueEnum.Boat)
                                wrongCells.Add(cells[k][j]);
                        }
                    }
                }
            }

            return wrongCells;
        }

        /// <summary>
        /// Реализация обобщенного GetEnumerator()
        /// </summary>
        /// <returns>Перечисление клеток Cell</returns>
        public IEnumerator<Cell> GetEnumerator()
        {
            for (int i = 1; i <= FieldSize; i++)
            {
                for (int j = 1; j <= FieldSize; j++)
                {
                    yield return cells[i][j];
                }
            }
        }

        /// <summary>
        /// Реализация GetEnumerator()
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
