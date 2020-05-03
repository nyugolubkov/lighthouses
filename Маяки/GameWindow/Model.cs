using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Маяки.GameWindow
{
    public class Model : OnPropertyChangedClass
    {
        readonly DeskOfCells _cells;

        public Model(DeskOfCells cells)
        {
            if (cells.Length != 144)
                throw new ArgumentException("Wrong cells Length!");

            _cells = new DeskOfCells(cells.Name);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    _cells[i, j] = cells[i, j];
                }
            }
        }

        public DeskOfCells Cells => _cells;
    }
}
