using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Маяки.GameWindow;

namespace LoadsSaver
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<Cell>> cells = new List<List<Cell>>(10);

            for (int i = 0; i < 10; i++)
            {
                List<Cell> cell = new List<Cell>(10);

                for (int j = 0; j < 10; j++)
                {
                    cell.Add(new Cell(i, j));
                }

                cells.Add(cell);
            }

            //cells[0][0].Value = CellValueEnum.Lighthouse;
            cells[1][1].Value = CellValueEnum.Lighthouse;
            cells[2][8].Value = CellValueEnum.Lighthouse;
            cells[3][6].Value = CellValueEnum.Lighthouse;
            cells[4][3].Value = CellValueEnum.Lighthouse;
            //cells[5][4].Value = CellValueEnum.Lighthouse;
            cells[6][5].Value = CellValueEnum.Lighthouse;
            cells[7][2].Value = CellValueEnum.Lighthouse;
            cells[8][4].Value = CellValueEnum.Lighthouse;
            cells[9][0].Value = CellValueEnum.Lighthouse;

            cells[0][4].Value = CellValueEnum.Unavailable;

            cells[1][1].AmountOfLightedBoats = 0;
            cells[2][8].AmountOfLightedBoats = 1;
            cells[3][6].AmountOfLightedBoats = 2;
            cells[4][3].AmountOfLightedBoats = 2;
            cells[6][5].AmountOfLightedBoats = 2;
            cells[7][2].AmountOfLightedBoats = 2;
            cells[8][4].AmountOfLightedBoats = 3;
            cells[9][0].AmountOfLightedBoats = 2;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<List<Cell>>));

            using (FileStream fs = new FileStream(@"..\..\..\Маяки\Levels\Level8.json",
                FileMode.Create))
            {
                ser.WriteObject(fs, cells);
            }
        }
    }
}
