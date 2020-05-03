using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Маяки.GameWindow
{
    /// <summary>
    /// Enum, отвечающий за значения состояния клетки
    /// </summary>
    public enum CellValueEnum
    {
        None,
        Boat,
        Blocked,
        Lighthouse,
        Unavailable
    }
}
