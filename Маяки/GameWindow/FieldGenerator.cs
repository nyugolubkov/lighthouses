using System;
using System.Collections.Generic;
using System.Linq;

namespace Маяки.GameWindow
{
    /// <summary>
    /// Реализует логику генерации новых полей приложения.
    /// </summary>
    public static class FieldGenerator
    {
        private static readonly Random rnd = new Random();

        /// <summary>
        /// Генерирует поля размера fieldSize x fieldSize
        /// </summary>
        /// <param name="fieldSize">Размер поля</param>
        /// <returns>Сгенерированное поле</returns>
        public static DeskOfCells Generator(int fieldSize)
        {
            // Экземпляр класса, в который будет записано сгенерированное поле
            DeskOfCells cells = new DeskOfCells("Головоломка", fieldSize);
            // Коллекция объектов, хранящее числа, отвечающие за некоторую клетку поля
            List<int> allElements = new List<int>(fieldSize * fieldSize);
            // Коллекция, хранящяя места, где точно не может быть кораблика
            HashSet<int> crosses = new HashSet<int>();

            // Заполнение коллекции числами от 0 до (размер поля)^2 для различия элементов
            for (int i = 0; i < fieldSize * fieldSize; i++)
            {
                allElements.Add(i);
            }

            // Цикл, отвечающий за генерацию корабликов на поле
            for (int i = 0; i < fieldSize; i++)
            {
                // Выбор случайного элемента из allElements
                int num = rnd.Next(allElements.Count);

                // Добавление кораблика в поле на место, 
                // соответствующее выбраннному элементу
                int row = allElements[num] / fieldSize,
                    column = allElements[num] % fieldSize;
                cells[row, column].Value = CellValueEnum.Boat;

                // Исключение из allElements элементов, соответствующих
                // кораблику и соседним с ним клеткам (крестикам)
                AddCrosses(fieldSize, allElements, num, crosses);
            }

            // Выбор количества маяков, которые будут сгенерированы
            int amountOfLighthouses = rnd.Next((int)(fieldSize * 8 / 10), fieldSize + 1);

            // Цикл, отвечающий за генерацию маяков на поле
            for (int i = 0; i < amountOfLighthouses; i++)
            {
                // Выбор случайного элемента из allElements
                int num = rnd.Next(allElements.Count);

                // Добавление маяка в поле на место, 
                // соответствующее выбраннному элементу
                int row = allElements[num] / fieldSize,
                    column = allElements[num] % fieldSize;
                cells[row, column].Value = CellValueEnum.Lighthouse;
                cells[row, column].AmountOfLightedBoats = cells.CountBoats(row, column);

                // Исключение из allElements элементов, соответствующих
                // маяку и соседним с ним клеткам (крестикам)
                AddCrosses(fieldSize, allElements, num, crosses);

                // Удаление из allElements элементов, находящихся на одной вертикали 
                // или диагонали для исключения возможности пересечения двух маяков
                for (int j = 0; j < fieldSize; j++)
                {
                    allElements.Remove(row * fieldSize + j);
                    allElements.Remove(j * fieldSize + column);
                }

                // Выход из цикла при недостатке свободных мест
                if (allElements.Count == 0)
                    break;
            }

            // Создание коллекции с элементами, соответствующими всем полям,
            // кроме корабликов и маяков, а также выбор количества мелей на поле
            List<int> unusedCells = (from el1 in allElements
                                     select el1).Concat
                                (from el2 in crosses
                                 select el2).ToList();
            int amountOfUnavailable = rnd.Next((int)(unusedCells.Count / 8));

            // Добавление мелей на поле
            for (int i = 0; i < amountOfUnavailable; i++)
            {
                // Выбор случайного элемента из unusedElements
                int num = rnd.Next(unusedCells.Count);

                // Добавление мели в поле на место, 
                // соответствующее выбраннному элементу
                int row = unusedCells[num] / fieldSize,
                    column = unusedCells[num] % fieldSize;
                cells[row, column].Value = CellValueEnum.Unavailable;

                // Удаление из unusedElevents элемента, соответствующего мели
                unusedCells.Remove(unusedCells[num]);
            }

            // Удаление мелей вокруг маяков
            foreach (var el in cells)
            {
                if (el.Value == CellValueEnum.Lighthouse)
                    cells.MakeCrosses(el.Row, el.Column);
            }

            // Очистка поля от корабликов и маяков и возвращение результата
            cells.Clear();
            return cells;
        }

        /// <summary>
        /// Удаляет всех соседей клетки, соответствующей числу num, 
        /// из коллекции чисел, соответствующих клеткам игрового поля
        /// </summary>
        /// <param name="fieldSize">Размер поля</param>
        /// <param name="allElements">Коллекция чисел, соответствующих клеткам поля</param>
        /// <param name="num">Число, соответствующее некой клетке поля</param>
        /// <param name="crosses">Коллекция чисел-соседей</param>
        private static void AddCrosses(int fieldSize, List<int> allElements,
            int num, HashSet<int> crosses)
        {
            if (IsCorner(fieldSize, allElements, allElements[num], crosses))
                return;

            if (IsBorder(fieldSize, allElements, allElements[num], crosses))
                return;

            int thisNum = allElements[num];

            allElements.Remove(thisNum);
            allElements.Remove(thisNum + 1);
            allElements.Remove(thisNum - 1);
            allElements.Remove(thisNum + fieldSize);
            allElements.Remove(thisNum + fieldSize + 1);
            allElements.Remove(thisNum + fieldSize - 1);
            allElements.Remove(thisNum - fieldSize);
            allElements.Remove(thisNum - fieldSize + 1);
            allElements.Remove(thisNum - fieldSize - 1);
            crosses.Add(thisNum + 1);
            crosses.Add(thisNum - 1);
            crosses.Add(thisNum + fieldSize);
            crosses.Add(thisNum + fieldSize + 1);
            crosses.Add(thisNum + fieldSize - 1);
            crosses.Add(thisNum - fieldSize);
            crosses.Add(thisNum - fieldSize - 1);
            crosses.Add(thisNum - fieldSize + 1);
        }

        /// <summary>
        /// Вспомогательный метод метода AddCrosses, который проверяет, является ли
        /// клетка, соответствующая числу num, угловой. Если да, то удаляет элементы,
        /// соответствующие соседним к клетке, соответствующей числу num
        /// </summary>
        /// <param name="fieldSize">Размер поля</param>
        /// <param name="allElements">Коллекция чисел</param>
        /// <param name="thisNum">Число из коллеции</param>
        /// <param name="crosses">Коллекция чисел-соседей</param>
        /// <returns>Результат проверки</returns>
        private static bool IsCorner(int fieldSize, List<int> allElements,
            int thisNum, HashSet<int> crosses)
        {
            if (thisNum == 0)
            {
                allElements.Remove(thisNum);
                allElements.Remove(thisNum + 1);
                allElements.Remove(thisNum + fieldSize);
                allElements.Remove(thisNum + fieldSize + 1);
                crosses.Add(thisNum + 1);
                crosses.Add(thisNum + fieldSize);
                crosses.Add(thisNum + fieldSize + 1);
                return true;
            }

            if (thisNum == fieldSize - 1)
            {
                allElements.Remove(thisNum);
                allElements.Remove(thisNum - 1);
                allElements.Remove(thisNum + fieldSize);
                allElements.Remove(thisNum + fieldSize - 1);
                crosses.Add(thisNum - 1);
                crosses.Add(thisNum + fieldSize);
                crosses.Add(thisNum + fieldSize - 1);
                return true;
            }

            if (thisNum == fieldSize * (fieldSize - 1))
            {
                allElements.Remove(thisNum);
                allElements.Remove(thisNum + 1);
                allElements.Remove(thisNum - fieldSize);
                allElements.Remove(thisNum - fieldSize + 1);
                crosses.Add(thisNum + 1);
                crosses.Add(thisNum - fieldSize);
                crosses.Add(thisNum - fieldSize + 1);
                return true;
            }

            if (thisNum == fieldSize * fieldSize - 1)
            {
                allElements.Remove(thisNum);
                allElements.Remove(thisNum - 1);
                allElements.Remove(thisNum - fieldSize);
                allElements.Remove(thisNum - fieldSize - 1);
                crosses.Add(thisNum - 1);
                crosses.Add(thisNum - fieldSize);
                crosses.Add(thisNum - fieldSize - 1);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Вспомогательный метод метода AddCrosses, который проверяет, является ли
        /// клетка, соответствующая числу num, находящейся с краю. Если да, то удаляет элементы,
        /// соответствующие соседним к клетке, соответствующей числу num
        /// </summary>
        /// <param name="fieldSize">Размер поля</param>
        /// <param name="allElements">Коллекция чисел</param>
        /// <param name="thisNum">Число из коллеции</param>
        /// <param name="crosses">Коллекция чисел-соседей</param>
        /// <returns>Результат проверки</returns>
        private static bool IsBorder(int fieldSize, List<int> allElements,
            int thisNum, HashSet<int> crosses)
        {
            if (thisNum < fieldSize)
            {
                allElements.Remove(thisNum);
                allElements.Remove(thisNum + 1);
                allElements.Remove(thisNum - 1);
                allElements.Remove(thisNum + fieldSize);
                allElements.Remove(thisNum + fieldSize + 1);
                allElements.Remove(thisNum + fieldSize - 1);
                crosses.Add(thisNum + 1);
                crosses.Add(thisNum - 1);
                crosses.Add(thisNum + fieldSize);
                crosses.Add(thisNum + fieldSize + 1);
                crosses.Add(thisNum + fieldSize - 1);
                return true;
            }

            if (thisNum % fieldSize == 0)
            {
                allElements.Remove(thisNum);
                allElements.Remove(thisNum + fieldSize);
                allElements.Remove(thisNum - fieldSize);
                allElements.Remove(thisNum + 1);
                allElements.Remove(thisNum + fieldSize + 1);
                allElements.Remove(thisNum - fieldSize + 1);
                crosses.Add(thisNum + fieldSize);
                crosses.Add(thisNum - fieldSize);
                crosses.Add(thisNum + 1);
                crosses.Add(thisNum + fieldSize + 1);
                crosses.Add(thisNum - fieldSize + 1);
                return true;
            }

            if ((thisNum + 1) % fieldSize == 0)
            {
                allElements.Remove(thisNum);
                allElements.Remove(thisNum + fieldSize);
                allElements.Remove(thisNum - fieldSize);
                allElements.Remove(thisNum - 1);
                allElements.Remove(thisNum - fieldSize - 1);
                allElements.Remove(thisNum + fieldSize - 1);
                crosses.Add(thisNum + fieldSize);
                crosses.Add(thisNum - fieldSize);
                crosses.Add(thisNum - 1);
                crosses.Add(thisNum - fieldSize - 1);
                crosses.Add(thisNum + fieldSize - 1);
                return true;
            }

            if (thisNum > fieldSize * (fieldSize - 1))
            {
                allElements.Remove(thisNum);
                allElements.Remove(thisNum + 1);
                allElements.Remove(thisNum - 1);
                allElements.Remove(thisNum - fieldSize);
                allElements.Remove(thisNum - fieldSize + 1);
                allElements.Remove(thisNum - fieldSize - 1);
                crosses.Add(thisNum + 1);
                crosses.Add(thisNum - 1);
                crosses.Add(thisNum - fieldSize);
                crosses.Add(thisNum - fieldSize + 1);
                crosses.Add(thisNum - fieldSize - 1);
                return true;
            }

            return false;
        }
    }
}