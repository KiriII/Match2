using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core.Board;
using Match3Core.RandomGenerate;

namespace Match3Core
{
    public static class CreateCellsOnBoard
    {
        public static void CreateBoard(Slot[,] slots, SwitchCellsContoller switchCellController)
        {
            var findedCells = new List<Slot>();

            foreach (var s in slots)
            {
                if (s.CanHoldCell && !s.IsBlocked && (s.Cell == null || s.Cell.color == CellsColor.Empty))
                {
                    findedCells.Add(s);
                }
            }

            foreach (var cell in findedCells)
            {
                Debug.Log(cell.Coordinate);
                switchCellController.SwitchWithNewCell(cell.Coordinate, 
                    RandomCellsGenerator.GenerateNewCell(getPreviousCellsColor(slots, cell.Coordinate), 2));
            }
        }

        private static List<CellsColor> getPreviousCellsColor(Slot[,] slots, Coordinate coordinate)
        {
            var previousColors = new List<CellsColor>();

            int x = coordinate.x;
            int y = coordinate.y;

            if(x - 1 >= 0)
            {
                previousColors.Add(slots[x - 1, y].Cell.color);
                if (x - 2 >= 0)
                {
                    previousColors.Add(slots[x - 2, y].Cell.color);
                }
            }
            if (y - 1 >= 0)
            {
                previousColors.Add(slots[x, y - 1].Cell.color);
                if (y - 2 >= 0)
                {
                    previousColors.Add(slots[x, y - 2].Cell.color);
                }
            }

            return previousColors;
        }
    }
}