using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core
{
    public static class CreateCellsOnBoard
    {
        public static List<Coordinate> CreateBoard(Slot[,] slots)
        {
            var findedCells = new List<Coordinate>();

            foreach (var s in slots)
            {
                if (s.CanHoldCell && (s.Cell == null || s.Cell.color == CellsColor.Empty))
                {
                    findedCells.Add(s.Coordinate);
                }
            }

            return findedCells;
        }
    }
}