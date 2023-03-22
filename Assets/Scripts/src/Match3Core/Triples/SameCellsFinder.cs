using System.Collections.Generic;
using System.Linq;
namespace Match3Core.Triples
{
    public static class SameCellsFinder
    {
        private const int CELLS_COUNT_IN_A_ROW = 3;

        public static List<int> CheckSameInArray(Cell[] cells)
        {
            var findedCells = new List<int>();
            for (int cellPosition = 0; cellPosition <= cells.Length - CELLS_COUNT_IN_A_ROW; cellPosition++)
            {
                var sameCellsCount = 1;

                if (cells[cellPosition] == null) continue;

                for (int nextCellPositon = cellPosition + 1; nextCellPositon < cells.Length; nextCellPositon++)
                {
                    if (!cells[cellPosition].Equals(cells[nextCellPositon]))
                    {
                        break;
                    }
                    sameCellsCount++;
                }

                if (sameCellsCount >= CELLS_COUNT_IN_A_ROW)
                {
                    var sameCells = Enumerable.Range(cellPosition, sameCellsCount).ToList();
                    findedCells.AddRange(sameCells);
                    cellPosition += sameCellsCount;
                }
            }
            return findedCells;
        }
    }
}