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
            var findedCells = new List<Coordinate>();

            foreach (var s in slots)
            {
                if (s.CanHoldCell && !s.IsBlocked && (s.Cell == null || s.Cell.color == CellsColor.Empty))
                {
                    findedCells.Add(s.Coordinate);
                }
            }

            foreach (var cell in findedCells)
            {
                switchCellController.SwitchWithNewCell(cell, RandomCellsGenerator.GenerateNewCell());
            }
        }
    }
}