using System;
using System.Collections.Generic;
using System.Linq;

namespace Match3Core.Triples
{
    public static class SameCellsFinder
    {
        private const int CELLS_COUNT_IN_A_ROW = 3;

        private static List<int> CheckSameInArray(Cell[] cells, Action<int> triplesCount)
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
                    triplesCount?.Invoke(sameCellsCount);
                }
            }
            return findedCells;
        }

        public static List<Coordinate> TriplesFinder(Cell[,] cells, Action<int> triplesCount = null)
        {
            var findedCells = new List<Coordinate>();

            var rows = cells.GetLength(0);
            var collumns = cells.GetLength(1);

            if (rows != collumns) throw new Exception($"Board have uncorrect size {rows}x{collumns}");

            for (int i = 0; i < rows; i++)
            {
                var YcoordinatesOfTriples = CheckSameInArray(GetFullRow(cells, i), triplesCount);
                var XcoordinatesOfTriples = CheckSameInArray(GetFullCollumn(cells, i), triplesCount);

                foreach (var y in YcoordinatesOfTriples)
                {
                    if (!FindCoordinateInFinded(findedCells, i, y))
                    {
                        findedCells.Add(new Coordinate(i, y));
                    }
                }
                foreach (var x in XcoordinatesOfTriples)
                {
                    if (!FindCoordinateInFinded(findedCells, x, i))
                    {
                        findedCells.Add(new Coordinate(x, i));
                    }
                }
            }
            return findedCells;
        }

        private static bool FindCoordinateInFinded(List<Coordinate> findedCells, int x, int y)
        {
            var contain = false;
            foreach (var c in findedCells)
            {
                if ((c.x == x) && (c.y == y))
                {
                    contain = true;
                }
            }
            return contain;
        }

        public static Cell[] GetFullRow(Cell[,] cells, int rowNumber)
        {
            var row = new Cell[cells.GetLength(0)];
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                row[i] = cells[rowNumber, i];
            }
            return row;
        }

        public static Cell[] GetFullCollumn(Cell[,] cells, int collumnNumber)
        {
            var collumn = new Cell[cells.GetLength(1)];
            for (int i = 0; i < cells.GetLength(1); i++)
            {
                collumn[i] = cells[i, collumnNumber];
            }
            return collumn;
        }
    }
}