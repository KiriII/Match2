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

        // weight от 1. Вес уменьшается на 1 за каждый повтор цвета
        public static Cell GenerateNewCell(List<CellsColor> colors, int weight)
        {
            if (weight <= 0) return null;

            var weightForColors = new List<int>();
            var allWeight = 0;

            foreach (var color in Enum.GetValues(typeof(CellsColor)))
            {
                if ((int)color == 0) continue;
                var colorWeight = weight;

                foreach (var c in colors)
                {
                    if ((int)c == (int)color && colorWeight >= 1)
                    {
                        colorWeight--;
                    }
                }

                allWeight += colorWeight;
                weightForColors.Add(colorWeight);
            }

            var rnd = new System.Random();
            var number = rnd.Next(1, allWeight);

            Debug.Log($"{String.Join(",", weightForColors)} {number}");
            for (int i = 0; i < weightForColors.Count; i++)
            {
                number -= weightForColors[i];
                if (number <= 0)
                {
                    return new Cell((CellsColor)i+1);
                }
            }
            return null;
        }
    }
}
