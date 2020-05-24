using System;

namespace Маяки.GameWindow
{
    /// <summary>
    /// Хранит в себе информацию о игровом поле и 
    /// является наследником класса OnPropertyChangedClass
    /// </summary>
    public class Model : OnPropertyChangedClass
    {
        readonly DeskOfCells _cells;

        /// <summary>
        /// Конструктор, инициализирующий элементы класса
        /// </summary>
        /// <param name="cells">Игровое поле</param>
        public Model(DeskOfCells cells)
        {
            if (!cells.IsCorrect())
                throw new ArgumentException("Wrong cells Length!");

            _cells = cells;
        }

        public DeskOfCells Cells => _cells;
    }
}
