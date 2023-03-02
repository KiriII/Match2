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
            var number = rnd.Next(1, 4);
            return new Cell((CellsColor)number);
        }
    }
}