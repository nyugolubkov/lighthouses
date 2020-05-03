using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Маяки.GameWindow
{
    /// <summary>
    /// Класс - коллекция, отвечающий за хранение информации об игровом поле и
    /// реализующий методы работы с полем. Также, реализует интерфейс IEnumerable
    /// </summary>
    public class DeskOfCells : IEnumerable<Cell>
    {
        private Cell[,] cells;

        public DeskOfCells(string name)
        {
            Name = name;
            cells = new Cell[12, 12];

            for (int i = 0; i < 12; i++)
            {
                cells[0, i] = new Cell(0, i);
                cells[i, 0] = new Cell(i, 0);
                cells[11, i] = new Cell(11, i);
                cells[i, 11] = new Cell(i, 11);
            }
        }

        public int Length => cells.Length;
        public string Name { get; }

        public Cell this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i > 9 || j > 9)
                    throw new FormatException("Out of array borders!");

                return cells[i + 1, j + 1];
            }
            set
            {
                if (i < 0 || j < 0 || i > 9 || j > 9)
                    throw new FormatException("Out of array borders!");

                cells[i + 1, j + 1] = value;
            }
        }

        public bool Check()
        {
            int count = 0;

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    if (cells[i, j].Value == CellValueEnum.Boat)
                        count++;

                    if (count > 10)
                        return false;

                    if (cells[i, j].Value == CellValueEnum.Boat &&
                        AreAnyBoatsNear(i, j))
                        return false;

                    if (cells[i, j].Value == CellValueEnum.Lighthouse &&
                        !IsLightHouseComplete(i, j))
                        return false;
                }
            }

            return count == 10;
        }

        public bool AreAnyBoatsNear(int row, int column)
        {
            return cells[row + 1, column].Value == CellValueEnum.Boat ||
                cells[row, column + 1].Value == CellValueEnum.Boat ||
                cells[row + 1, column + 1].Value == CellValueEnum.Boat ||
                cells[row - 1, column].Value == CellValueEnum.Boat ||
                cells[row, column - 1].Value == CellValueEnum.Boat ||
                cells[row - 1, column - 1].Value == CellValueEnum.Boat ||
                cells[row + 1, column - 1].Value == CellValueEnum.Boat ||
                cells[row - 1, column + 1].Value == CellValueEnum.Boat || 
                cells[row + 1, column].Value == CellValueEnum.Lighthouse ||
                cells[row, column + 1].Value == CellValueEnum.Lighthouse ||
                cells[row + 1, column + 1].Value == CellValueEnum.Lighthouse ||
                cells[row - 1, column].Value == CellValueEnum.Lighthouse ||
                cells[row, column - 1].Value == CellValueEnum.Lighthouse ||
                cells[row - 1, column - 1].Value == CellValueEnum.Lighthouse ||
                cells[row + 1, column - 1].Value == CellValueEnum.Lighthouse ||
                cells[row - 1, column + 1].Value == CellValueEnum.Lighthouse;
        }

        public bool IsLightHouseComplete(int row, int column)
        {
            int count = 0;

            for (int i = 1; i <= 10; i++)
            {
                if (cells[i, column].Value == CellValueEnum.Boat)
                    count++;
            }

            for (int i = 1; i <= 10; i++)
            {
                if (cells[row, i].Value == CellValueEnum.Boat)
                    count++;
            }

            return count == cells[row, column].AmountOfLightedBoats;
        }

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

        public IEnumerator<Cell> GetEnumerator()
        {
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    yield return cells[i, j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
