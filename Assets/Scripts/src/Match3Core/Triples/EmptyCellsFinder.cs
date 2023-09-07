using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;

public static class EmptyCellsFinder
{
    public static List<Coordinate> FindEmpty(Slot[,] slots)
    {
        var emptyCells = new List<Coordinate>();

        var rows = slots.GetLength(0);
        var collumns = slots.GetLength(1);

        if (rows != collumns) throw new Exception($"Board have uncorrect size {rows}x{collumns}");

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < collumns; j++)
            {
                if (slots[i,j].CanHoldCell && !slots[i,j].IsBlocked && slots[i,j].Cell.Color == CellsColor.Empty)
                {
                    emptyCells.Add(new Coordinate(i, j));
                }
            }
        }

        return emptyCells;
    }
}
