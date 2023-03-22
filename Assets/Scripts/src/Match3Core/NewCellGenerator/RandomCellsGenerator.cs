using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;


// -------- TODO Normal realisation of random generator of cells --------
namespace Match3Core.RandomGenerate
{
    public static class RandomCellsGenerator
    {
        public static Cell GenerateNewCell()
        {
            var rnd = new System.Random();
            var colorCount = Enum.GetValues(typeof(CellsColor)).Length;
            var number = rnd.Next(1, colorCount);
            return new Cell((CellsColor)number);
        }
    }
}
