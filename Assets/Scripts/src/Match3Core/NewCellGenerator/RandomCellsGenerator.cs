using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3Core;

namespace Match3Core.RandomGenerate
{
    public static class RandomCellsGenerator
    {
        public static Cell GenerateNewCell()
        {
            var rnd = new System.Random();
            var colorCount = Enum.GetValues(typeof(CellsColor)).Length;
            var number = rnd.Next(2, colorCount);
            return new Cell((CellsColor)number);
        }

        // weight �� 1. ��� ����������� �� 1 �� ������ ������ �����
        public static Cell GenerateNewCell(List<CellsColor> colors, int weight)
        {
            if (weight <= 0) return null;

            var weightForColors = new List<int>();
            var allWeight = 0;

            foreach (var color in Enum.GetValues(typeof(CellsColor)))
            {
                if ((CellsColor)color == CellsColor.Empty || (CellsColor)color == CellsColor.Special) continue;
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
            if (allWeight < 1) return GenerateNewCell();
            var rnd = new System.Random();
            var number = rnd.Next(1, allWeight);

            for (int i = 0; i < weightForColors.Count; i++)
            {
                number -= weightForColors[i];
                if (number <= 0)
                {
                    return new Cell((CellsColor)i+2);
                }
            }
            return null;
        }
    }
}
